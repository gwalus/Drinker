import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';
import { CoctailService } from 'src/app/_services/coctail.service';

@Component({
  selector: 'app-coctail-builder',
  templateUrl: './coctail-builder.component.html',
  styleUrls: ['./coctail-builder.component.css', '../style.css']
})
export class CoctailBuilderComponent implements OnInit {

  constructor(private coctailService: CoctailService, private coctail:FormBuilder) {
  
    this.CreateCoctailForm = this.coctail.group({
      coctailName: '',
      alcoholic: '',
      category:'',
      validatedCustomFile:'',
      glass: '',
      instructions: '',
      ingredients: this.coctail.array([]),
    });
    this.addIngredient();
  }

  ngOnInit(): void {
  }

  CreateCoctailForm: FormGroup;
  categoriesList: string[];
  glassesList: string[];

  ingredients() : FormArray {
    return this.CreateCoctailForm.get("ingredients") as FormArray
  }
   
  newIngredient(): FormGroup {
    return this.coctail.group({
      name: '',
      measure: '',
    })
  }
   
  addIngredient() {
    this.ingredients().push(this.newIngredient());
  }
   
  removeIngredient(i:number) {
    this.ingredients().removeAt(i);
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
