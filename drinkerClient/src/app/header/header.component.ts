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
    this.coctailService.getRandomCoctails(3).subscribe(randomCoctails => {
      console.log(randomCoctails as Coctail);
    })

    this.coctailService.getCoctailCategories().subscribe(categories => {
      console.log(categories);
    })

    this.coctailService.getCoctailGlasses().subscribe(glasses => {
      console.log(glasses);
    })
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
      'Vodka'
    ];

    let coctailParams: CoctailParams = new CoctailParams();

    coctailParams.alcoholic = 'Alcoholic';
    coctailParams.category = 'Ordinary Drink';
    coctailParams.pageSize = 3;
    coctailParams.pageNumber = 3;

    this.coctailService.getCoctailsByIngredients(ingredients, coctailParams).subscribe(coctails => {
      this.coctails = coctails.result;
      this.pagination = coctails.pagination

      console.log(this.coctails)
    })
  }
}
