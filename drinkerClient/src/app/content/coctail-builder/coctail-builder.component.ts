import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { CoctailService } from 'src/app/_services/coctail.service';
import { formatDate } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { FileItem, FileUploader } from 'ng2-file-upload';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { take } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';
import { Coctail } from 'src/app/_models/coctail';

@Component({
  selector: 'app-coctail-builder',
  templateUrl: './coctail-builder.component.html',
  styleUrls: ['./coctail-builder.component.css', '../style.scss']
})
export class CoctailBuilderComponent implements OnInit {
  uploader: FileUploader;
  user: User;
  currentIdForAddPhoto: number;
  photoMode = false;
  coctails: Coctail;
  photo: FileItem;

  constructor(private coctailService: CoctailService, private coctail: FormBuilder, private http: HttpClient, private accountService: AccountService,
    private toastr: ToastrService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user as User);

    let today = new Date().toISOString().slice(0, 10) + ' ' + new Date().toISOString().slice(11, 20);

    this.CreateCoctailForm = this.coctail.group({
      name: '',
      category: 'Cocktail',
      alcoholic: 'Alcoholic',
      glass: 'Highball glass',
      instructions: '',
      ingradients: this.coctail.array([]),
    });
    this.addIngredient();
  }

  ngOnInit(): void {
    this.initializeUploader();

    this.getCoctailGlasses();
    this.getCoctailCategories();
  }

  initializeUploader() {
    this.uploader = new FileUploader({
      url: 'https://localhost:5001/api/v1/cocktails/photo-to-cocktail',
      authToken: 'Bearer ' + this.user.token,
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024
    });

    this.uploader.onBuildItemForm = (item, form) => {
      form.append('cocktailId', this.currentIdForAddPhoto.toString());
    };

    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false;
      this.photo = file;
    }

    this.uploader.onSuccessItem = (item, response, status, headers) => {
      if (response) {
        this.toastr.success('Photo uploaded successfully')
        this.CreateCoctailForm.reset();
        this.currentIdForAddPhoto = 0;
        this.photoMode = false;
        this.CreateCoctailForm.enable();
      }
    }
  }

  CreateCoctailForm: FormGroup;
  categoriesList: string[];
  alcoholicList: string[] = ["Alcoholic", "Non alcoholic", "Optional alcohol"];

  glassesList: string[];
  selectedGlasses: string[];

  ingradients(): FormArray {
    return this.CreateCoctailForm.get("ingradients") as FormArray
  }

  newIngredient(): FormGroup {
    return this.coctail.group({
      name: '',
      measure: '',
    })
  }

  addIngredient() {
    this.ingradients().push(this.newIngredient());
  }

  removeIngredient(i: number) {
    this.ingradients().removeAt(i);
  }

  addCocktail() {
    this.coctailService.addCocktail(this.CreateCoctailForm.value).subscribe(id => {
      this.toastr.success('Your cocktail has been added, please add photo now!');
      this.currentIdForAddPhoto = id;
      this.photoMode = true;
      this.CreateCoctailForm.disable();
    },
      error => console.log(error))
  }

  getCoctailCategories() {
    this.coctailService.getCoctailCategories().subscribe(category => {
      this.categoriesList = category;
    })
  }

  getCoctailGlasses() {
    this.coctailService.getCoctailGlasses().subscribe(glass => {
      this.glassesList = glass;
    })
  }

}
