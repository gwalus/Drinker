import { NgModule } from '@angular/core';
import { TestBed } from '@angular/core/testing';
import { RouterModule, Routes } from '@angular/router';
import { CoctailBuilderComponent } from './content/coctail-builder/coctail-builder.component';

const routes: Routes = [
  {
    path: 'coctailBuilder',
    component: CoctailBuilderComponent
    }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
