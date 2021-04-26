import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
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
  allCoctails: Coctail[] = [];
  pagination: Pagination;
  paginationForAll: Pagination;

  constructor(private coctailService: CoctailService, private modalService: NgbModal) { }

  ngOnInit(): void {
  }

  openLoginContent(loginContent: any) {
    this.modalService.open(loginContent, { scrollable: true });
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
      'Vodka', 'Gin'
    ];

    let coctailParams: CoctailParams = new CoctailParams();
    // coctailParams.alcoholic = 'Alcoholic';
    // coctailParams.category = 'Ordinary Drink';
    console.log(coctailParams);

    this.coctailService.getCoctailsByIngredients(ingredients, coctailParams).subscribe(coctails => {
      this.coctails = coctails.result;
      this.pagination = coctails.pagination
      console.log(this.coctails);
    })
  }

  testAll() {
    let coctailParams: CoctailParams = new CoctailParams();

    this.coctailService.getAll(coctailParams).subscribe(coctails => {
      this.allCoctails = coctails.result;
      this.paginationForAll = coctails.pagination
      console.log(this.allCoctails);
    })
  }
}
