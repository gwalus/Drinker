import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { CoctailService } from 'src/app/_services/coctail.service';
import {formatDate} from '@angular/common';

@Component({
  selector: 'app-coctail-builder',
  templateUrl: './coctail-builder.component.html',
  styleUrls: ['./coctail-builder.component.css', '../style.css']
})
export class CoctailBuilderComponent implements OnInit {

  constructor(private coctailService: CoctailService, private coctail:FormBuilder) {
  
    let today = new Date().toISOString().slice(0, 10) + ' ' + new Date().toISOString().slice(11, 20);

    this.CreateCoctailForm = this.coctail.group({
      name: '',
      category:'',
      alcoholic: '',
      glass: '',
      instructions: '',
      photoUrl: '',
      dateModified: today,
      validatedCustomFile:'',
      ingradients: this.coctail.array([]),
      userId: '',
      isAccepted: true
    });
    this.addIngredient();
  }

  ngOnInit(): void {
    this.getCoctailGlasses();
    this.getCoctailCategories();
  }

  CreateCoctailForm: FormGroup;
  categoriesList: string[];
  alcoholicList: string[] = ["Alcoholic", "Non alcoholic",  "Optional alcohol"];

  glassesList: string[];
  selectedGlasses: string[];

  ingradients() : FormArray {
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
   
  removeIngredient(i:number) {
    this.ingradients().removeAt(i);
  }
   
  onSubmit() {
    console.log(this.CreateCoctailForm.value);
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
