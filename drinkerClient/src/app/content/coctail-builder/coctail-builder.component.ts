import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { CoctailService } from 'src/app/_services/coctail.service';
import { formatDate } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { FileUploader } from 'ng2-file-upload';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { take } from 'rxjs/operators';

@Component({
  selector: 'app-coctail-builder',
  templateUrl: './coctail-builder.component.html',
  styleUrls: ['./coctail-builder.component.css', '../style.css']
})
export class CoctailBuilderComponent implements OnInit {
  uploader: FileUploader;
  user: User;

  constructor(private coctailService: CoctailService, private coctail: FormBuilder, private http: HttpClient, private accountService: AccountService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user as User);

    let today = new Date().toISOString().slice(0, 10) + ' ' + new Date().toISOString().slice(11, 20);

    this.CreateCoctailForm = this.coctail.group({
      name: '',
      category: '',
      alcoholic: '',
      glass: '',
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
      url: 'http://localhost:36978/api/v1/cocktails/addcoctail',
      authToken: 'Bearer ' + this.user.token,
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024
    });

    // this.uploader.onBuildItemForm = (item, form) => {
    //   form.append('name', this.CreateCoctailForm.controls['name'].value);
    //   form.append('category', this.CreateCoctailForm.controls['category'].value);
    //   form.append('alcoholic', this.CreateCoctailForm.controls['alcoholic'].value);
    //   form.append('glass', this.CreateCoctailForm.controls['glass'].value);
    //   form.append('instructions', this.CreateCoctailForm.controls['instructions'].value);
    //   form.append('ingradients', JSON.stringify(this.CreateCoctailForm.controls['ingradients'].value));
    // };

    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false;
    }

    this.uploader.onSuccessItem = (item, response, status, headers) => {
      if (response) {
        console.log(response);
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

  onSubmit() {
    this.coctailService.addCocktail(this.CreateCoctailForm.value).subscribe(() =>
      console.log('Your cocktail has been added, please add photo now!'),
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
