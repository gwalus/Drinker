import { Component, OnInit } from '@angular/core';
import { Coctail } from 'src/app/_models/coctail';
import { CoctailParams } from 'src/app/_models/coctailParams';
import { CoctailService } from 'src/app/_services/coctail.service';
import { Router,ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-drinks',
  templateUrl: './drinks.component.html',
  styleUrls: ['./drinks.component.scss', '../style.css']
})

export class DrinksComponent implements OnInit {




  constructor(private coctailService: CoctailService,
              private _Activatedroute:ActivatedRoute,
              private _router:Router) { }

  sub: Subscription;
  searchKeyword: string;
  allCoctails: Coctail[] = [];
  coctailParams: CoctailParams = new CoctailParams();
  categories = new FormControl();
  categoriesList: string[];

  
  ngOnInit(): void {
    this.sub=this._Activatedroute.paramMap.subscribe(params => { 
      console.log(params);
       this.searchKeyword = params.get('keyword'); 
   });

   if(!this.searchKeyword)this.getCoctails();
   else this.getCoctailByName();

   this.getCoctailCategories();
  }

  getCoctails() {
    this.coctailService.getAll(this.coctailParams).subscribe(coctails => {
      this.allCoctails = this.allCoctails.concat(coctails.result);
      console.log(this.allCoctails);
    })
  }

  getCoctailByName() {
    this.coctailService.searchCoctailByName(this.searchKeyword).subscribe(coctails => {
      this.allCoctails = this.allCoctails.concat(coctails);
    })
  }

  loadMoreCoctails(){
    if(!this.searchKeyword){
      this.coctailParams.pageNumber++;
      this.getCoctails();
    }
  }

  getCoctailCategories(){
    this.coctailService.getCoctailCategories().subscribe(category => {
      this.categoriesList = category;
    })
  }

}
