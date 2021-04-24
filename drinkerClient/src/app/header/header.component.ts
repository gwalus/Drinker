import { Component, OnInit } from '@angular/core';
import { Coctail } from '../_models/coctail';
import { CoctailParams } from '../_models/coctailParams';
import { Pagination } from '../_models/pagination';
import { CoctailService } from '../_services/coctail.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  coctail: Coctail;
  coctails: Coctail[] = [];
  pagination: Pagination;

  constructor(private coctailService: CoctailService) { }

  ngOnInit(): void {
  }

  testByName(name: string = 'Mojito') {
    this.coctailService.searchCoctailByName(name).subscribe(response => {
      let coctail = response as Coctail;
      this.coctail = coctail;
      console.log(this.coctail);
    }, error => {
      console.log(error);
    });
  }

  testByIngredients() {
    const ingredients: string[] = [
      'Vodka'
    ];

    let coctailParams: CoctailParams = new CoctailParams();
    console.log(coctailParams);

    this.coctailService.getCoctailsByIngredients(ingredients, coctailParams).subscribe(coctails => {
      this.coctails = coctails.result;
      this.pagination = coctails.pagination
      console.log(this.coctails);
    })
  }
}
