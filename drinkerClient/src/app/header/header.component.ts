import { Component, OnInit } from '@angular/core';
import { Coctail } from '../_models/coctail';
import { CoctailService } from '../_services/coctail.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  coctail: Coctail;

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
}
