import { Component, OnInit } from '@angular/core';
import { Coctail } from 'src/app/_models/coctail';
import { CoctailParams } from 'src/app/_models/coctailParams';
import { Pagination } from 'src/app/_models/pagination';
import { CoctailService } from 'src/app/_services/coctail.service';

@Component({
  selector: 'app-drinks',
  templateUrl: './drinks.component.html',
  styleUrls: ['./drinks.component.scss', '../style.css']

})
export class DrinksComponent implements OnInit {

  constructor(private coctailService: CoctailService) { }

  allCoctails: Coctail[] = [];
  pagination: Pagination;
  paginationForAll: Pagination;
  
  ngOnInit(): void {
    this.getCoctails();
  }

  getCoctails() {
    let coctailParams: CoctailParams = new CoctailParams();

    this.coctailService.getAll(coctailParams).subscribe(coctails => {
      this.allCoctails = coctails.result;
      this.paginationForAll = coctails.pagination
      console.log(this.allCoctails);
    })
  }
}
