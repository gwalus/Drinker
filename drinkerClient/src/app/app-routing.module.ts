import { NgModule } from '@angular/core';
import { TestBed } from '@angular/core/testing';
import { RouterModule, Routes } from '@angular/router';
import { CoctailBuilderComponent } from './content/coctail-builder/coctail-builder.component';
import { DrinksComponent } from './content/drinks/drinks.component';
import { GameComponent } from './content/game/game.component';
import { CalculatorComponent } from './content/calculator/calculator.component';

const routes: Routes = [
  {
    path: 'coctailBuilder',
    component: CoctailBuilderComponent
  },
  {
    path: 'drinks',
    component: DrinksComponent
  },
  {
    path: 'game',
    component: GameComponent
  },
  {
    path: 'calculator',
    component: CalculatorComponent
  }
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
