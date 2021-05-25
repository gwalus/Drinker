import { Component, OnInit } from '@angular/core';
import { Coctail } from 'src/app/_models/coctail';
import { CoctailParams } from 'src/app/_models/coctailParams';
import { CoctailService } from 'src/app/_services/coctail.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-drinks',
  templateUrl: './drinks.component.html',
  styleUrls: ['./drinks.component.scss', '../style.css']
})

export class DrinksComponent implements OnInit {

  constructor(private coctailService: CoctailService,
    private _Activatedroute: ActivatedRoute,
    private _router: Router) { }

  sub: Subscription;
  searchKeyword: string = "";
  viewKeyword: boolean = false;

  viewCoctails: Coctail[] = [];

  coctailParams: CoctailParams = new CoctailParams();

  coctailByName: Coctail[] = [];
  allCoctails: Coctail[] = [];

  categories = new FormControl();
  categoriesList: string[];
  selectedCategories: string[];

  glasses = new FormControl();
  glassesList: string[];
  selectedGlasses: string[];

  alcoholic = new FormControl();
  alcoholicList: string[] = ["Alcoholic", "Non alcoholic", "Optional alcohol"];
  selectedAlcoholic: string[];

  ngOnInit(): void {
    this.getCoctailCategories();
    this.getCoctailGlasses();

    this.sub = this._Activatedroute.paramMap.subscribe(params => {
      if (params.get('keyword') != "null") {
        this.searchKeyword = params.get('keyword');
      }
    });

    //this.getCoctailByName();
    this.getCoctailByFilter();
  }

  async getCoctailByName() {
    this.coctailService.searchCoctailByName(this.searchKeyword).subscribe((coctails) => {
      this.coctailByName = coctails;
    })
  }

  loadMoreCoctails() {
    this.coctailParams.pageNumber++;
    this.getCoctails();
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

  async getCoctailByFilter() {
    this.coctailParams.categories = this.selectedCategories;
    this.coctailParams.glasses = this.selectedGlasses;
    this.coctailParams.alcoholicTypes = this.selectedAlcoholic;
    this.coctailParams.pageNumber = 1;

    if (!this.searchKeyword) {
      this.coctailService.getAll(this.searchKeyword, [], this.coctailParams).subscribe(coctails => {
        this.viewCoctails = coctails.result;
      })
    }

    else {
      await this.getCoctailByName();
      this.viewKeyword = true;
      this.viewCoctails = [];

      if (this.coctailParams.categories || this.coctailParams.glasses || this.coctailParams.alcoholicTypes) {
        if (this.coctailParams.categories) {
          this.coctailParams.categories.forEach(element => {
            this.viewCoctails = this.viewCoctails.concat(this.coctailByName.filter(c => c.category == element));
          });
          this.coctailByName = this.viewCoctails;
        }

        if (this.coctailParams.glasses) {
          this.viewCoctails = [];
          this.coctailParams.glasses.forEach(element => {
            this.viewCoctails = this.viewCoctails.concat(this.coctailByName.filter(c => c.glass == element));
          });
          this.coctailByName = this.viewCoctails;
        }

        if (this.coctailParams.alcoholicTypes) {
          this.viewCoctails = [];
          this.coctailParams.alcoholicTypes.forEach(element => {
            this.viewCoctails = this.viewCoctails.concat(this.coctailByName.filter(c => c.alcoholic == element));
          });
          this.coctailByName = this.viewCoctails;
        }

        this.viewCoctails = this.coctailByName;
      }
      else {
        await this.getCoctailByName();
        this.viewCoctails = this.coctailByName;
      }

    }
  }

  getCoctails() {
    this.coctailService.getAll(this.searchKeyword, [], this.coctailParams).subscribe(coctails => {
      this.viewCoctails = this.viewCoctails.concat(coctails.result);
    })
  }

  clearFilter() {
    this.selectedCategories = undefined;
    this.selectedGlasses = undefined;
    this.selectedAlcoholic = undefined;
    this.viewCoctails = [];
    this.searchKeyword = undefined;
    this.coctailByName = [];
    this.coctailParams.pageNumber = 1;
    this.viewKeyword = false;
    this.getCoctailByFilter();
  }

  deleteKeyword() {
    this.searchKeyword = "";
    this.viewKeyword = false;
    this.coctailByName = [];
    this.getCoctailByFilter();
  }
}
