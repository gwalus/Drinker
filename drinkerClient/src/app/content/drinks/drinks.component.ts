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
  page = 4;
  coctailParams: CoctailParams = new CoctailParams();
  
  ngOnInit(): void {
    this.getCoctails();
  }

  getCoctails() {
    this.coctailService.getAll(this.coctailParams).subscribe(coctails => {
      this.allCoctails = coctails.result;
      this.pagination = coctails.pagination
      console.log(this.allCoctails);
      console.log(this.pagination);
    })
  }
}
