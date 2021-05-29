import { Component, OnInit } from '@angular/core';
import { Coctail } from 'src/app/_models/coctail';
import { CoctailParams } from 'src/app/_models/coctailParams';
import { PaginationParams } from 'src/app/_models/paginationParams';
import { CoctailService } from 'src/app/_services/coctail.service';

@Component({
  selector: 'app-favorite-drink',
  templateUrl: './favorite-drink.component.html',
  styleUrls: ['./favorite-drink.component.scss', '../style.css']
})
export class FavoriteDrinkComponent implements OnInit {

  constructor(private coctailService: CoctailService) { }

  ngOnInit(): void {
    this.getFavouritedCocktails();
    this.paginationParams.pageSize = 12;
    this.paginationParams.pageNumber = 1;

  }

  paginationParams: PaginationParams = new PaginationParams();
  viewCoctails: Coctail[] = [];
  
  getFavouritedCocktails() {
    this.coctailService.getFavouritedCocktails(this.paginationParams).subscribe(coctails => {
      if(coctails.result.length>0)
        this.viewCoctails = coctails.result;
    })
  }

  addCoctails(){
    this.coctailService.getFavouritedCocktails(this.paginationParams).subscribe(coctails => {
      if(coctails.result.length>0)
        this.viewCoctails = this.viewCoctails.concat(coctails.result);
    })
  }

  loadMoreCoctails() {
    this.paginationParams.pageNumber++;
    this.addCoctails();
  }

}
