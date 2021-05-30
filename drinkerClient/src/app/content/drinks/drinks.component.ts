import { Component, OnInit } from '@angular/core';
import { Coctail } from 'src/app/_models/coctail';
import { CoctailParams } from 'src/app/_models/coctailParams';
import { CoctailService } from 'src/app/_services/coctail.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { FormControl } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-drinks',
  templateUrl: './drinks.component.html',
  styleUrls: ['./drinks.component.scss', '../style.scss']
})

export class DrinksComponent implements OnInit {

  constructor(
    private toastr: ToastrService, 
    private coctailService: CoctailService,
    private _Activatedroute: ActivatedRoute,
    private _router: Router) { }

  sub: Subscription;

  viewKeyword: boolean = false;

  viewCoctails: Coctail[] = [];

  coctailParams: CoctailParams = new CoctailParams();
  searchKeyword: string = '';

  categories = new FormControl();
  categoriesList: string[];
  selectedCategories: string[] = [];

  glasses = new FormControl();
  glassesList: string[];
  selectedGlasses: string[] = [];

  alcoholic = new FormControl();
  alcoholicList: string[] = ["Alcoholic", "Non alcoholic", "Optional alcohol"];
  selectedAlcoholic: string[] = [];

  ingredients = new FormControl();
  ingredientsList: string[];
  selectedIngredients: string[] = [];

  ngOnInit(): void {
    this.getCoctailCategories();
    this.getCoctailGlasses();
    this.getCoctailIngredients();

    this.sub = this._Activatedroute.paramMap.subscribe(params => {
      if (params.get('keyword') != null) {
        this.searchKeyword = params.get('keyword');
      }
    });

    this.getCoctailByFilter();
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

  getCoctailIngredients() {
    this.coctailService.getIngredientNames().subscribe(ingredients => {
      this.ingredientsList = ingredients;
    })
  }

  loadMoreCoctails() {
    this.coctailParams.pageNumber++;
    this.addCoctails();
  }

  getCoctails() {
    this.coctailService.getAll(this.searchKeyword, this.selectedIngredients, this.coctailParams).subscribe(coctails => {
      if(coctails.result.length<=0)
        this.toastr.error('Not found cocktails');
      this.viewCoctails = coctails.result;
    })
  }

  addCoctails() {
    this.coctailService.getAll(this.searchKeyword, this.selectedIngredients, this.coctailParams).subscribe(coctails => {
      this.viewCoctails = this.viewCoctails.concat(coctails.result);
    })
  }

  getCoctailByFilter() {
    this.coctailParams.categories = this.selectedCategories;
    this.coctailParams.glasses = this.selectedGlasses;
    this.coctailParams.alcoholicTypes = this.selectedAlcoholic;
    this.coctailParams.pageNumber = 1;

    this.getCoctails();

    if (this.searchKeyword) {
      this.viewKeyword = true;
    }
  }

  clearFilter() {
    this.selectedCategories = [];
    this.selectedIngredients = [];
    this.selectedGlasses = [];
    this.selectedAlcoholic = [];
    this.viewCoctails = [];
    this.searchKeyword = '';
    this.coctailParams.pageNumber = 1;
    this.viewKeyword = false;
    this.getCoctailByFilter();
  }

  deleteKeyword() {
    this.searchKeyword = '';
    this.viewKeyword = false;
    this.getCoctailByFilter();
  }
}
