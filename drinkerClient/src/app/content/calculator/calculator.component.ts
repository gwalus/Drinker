import { Component, OnInit } from '@angular/core';
import { Calculator } from 'src/app/_models/calculator';

@Component({
  selector: 'app-calculator',
  templateUrl: './calculator.component.html',
  styleUrls: ['./calculator.component.css']
})
export class CalculatorComponent {
  
  calcModel = new Calculator();
  
  alcoholProcentToGram(percentage: number, quantity: number){
    if(percentage != 0 && quantity !=0){
      let x: number = percentage * quantity * 0.8 / 100;
      return x;
    }
    return 0;
  }

  calculate(){
    
    let denominator: number;
    let combustion: number = 0.20 * this.calcModel.timePassed; 

    this.calcModel.gram = this.alcoholProcentToGram(this.calcModel.percentage, this.calcModel.quantity);

    if(this.calcModel.sex == 1) denominator = 0.7 * this.calcModel.weight;
    else denominator = 0.6 * this.calcModel.weight;

    this.calcModel.permile = parseFloat(((this.calcModel.gram / denominator) - combustion).toFixed(2));

    if(this.calcModel.permile < 0) this.calcModel.permile = 0;
  }

}
