import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-coctail-builder',
  templateUrl: './coctail-builder.component.html',
  styleUrls: ['./coctail-builder.component.css', '../style.css']
})
export class CoctailBuilderComponent implements OnInit {

  constructor(private coctail:FormBuilder) {
  
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

  CreateCoctailForm: FormGroup;

  ngOnInit(): void {
  }

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

}
