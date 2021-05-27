import { Component, OnInit } from '@angular/core';
import { Coctail } from '../_models/coctail';
import { AccountService } from '../_services/account.service';
import { CoctailService } from '../_services/coctail.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  carusele: Coctail[];
  randomCoctails: Coctail[];

  constructor(public accountService: AccountService, private coctailService: CoctailService) { }

  ngOnInit(): void {
    this.getRandomCoctails();
  }

  logout() {
    this.accountService.logout();
  }

  getRandomCoctails(){
    this.coctailService.getRandomCoctails(12).subscribe(coctails => {
      if(coctails){
        this.randomCoctails = coctails;
        this.carusele = this.randomCoctails.filter((u, i) => i < 3);
      }
    })
  }

}
