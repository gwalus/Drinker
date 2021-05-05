import { Component, OnInit } from '@angular/core';
import { Coctail } from 'src/app/_models/coctail';
import { CoctailParams } from 'src/app/_models/coctailParams';
import { CoctailService } from 'src/app/_services/coctail.service';
import { Router,ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';

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
  
  ngOnInit(): void {
    
    this.sub=this._Activatedroute.paramMap.subscribe(params => { 
      console.log(params);
       this.searchKeyword = params.get('keyword'); 
   });

   if(!this.searchKeyword)this.getCoctails();
   else this.getCoctailByName();
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

  LoadMoreCoctails(){
    if(!this.searchKeyword){
      this.coctailParams.pageNumber++;
      this.getCoctails();
    }
  }
}
