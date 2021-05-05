import { Component, OnInit } from '@angular/core';
import { Coctail } from 'src/app/_models/coctail';
import { CoctailParams } from 'src/app/_models/coctailParams';
import { Pagination } from 'src/app/_models/pagination';
import { CoctailService } from 'src/app/_services/coctail.service';
import {PageEvent} from '@angular/material/paginator';

@Component({
  selector: 'app-drinks',
  templateUrl: './drinks.component.html',
  styleUrls: ['./drinks.component.scss', '../style.css']

})
export class DrinksComponent implements OnInit {

  constructor(private coctailService: CoctailService) { }

  allCoctails: Coctail[] = [];
  coctailParams: CoctailParams = new CoctailParams();
  
  ngOnInit(): void {
    this.getCoctails();
  }

  getCoctails() {
    this.coctailService.getAll(this.coctailParams).subscribe(coctails => {
      this.allCoctails = this.allCoctails.concat(coctails.result);
      console.log(this.allCoctails);
    })
  }

  LoadMoreCoctails(){
    this.coctailParams.pageNumber++;
    this.getCoctails();
  }

  onScroll() {
    console.log('scrolled!!');
  }
}
