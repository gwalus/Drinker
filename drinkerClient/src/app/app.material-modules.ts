import { NgModule } from '@angular/core';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';

@NgModule({
    exports: [
        MatIconModule,
        MatSelectModule,
        MatInputModule,
        MatAutocompleteModule
    ]
})
export class AppMaterialModule { }