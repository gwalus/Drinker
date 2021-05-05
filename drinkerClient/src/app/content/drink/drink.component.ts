import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Coctail } from 'src/app/_models/coctail';
import { CoctailService } from 'src/app/_services/coctail.service';
import { NumberLiteralType } from 'typescript';

@Component({
  selector: 'app-drink',
  templateUrl: './drink.component.html',
  styleUrls: ['./drink.component.css', '../style.css']
})

export class DrinkComponent implements OnInit {

  constructor(private coctailService: CoctailService,
              private _Activatedroute:ActivatedRoute,
              private _router:Router) { }

  id: number;
  coctail: Coctail;
  sub: Subscription;

  ngOnInit(): void {
    this.sub=this._Activatedroute.paramMap.subscribe(params => { 
      this.id = Number(params.get('id')); 
   });

   this.getCoctailById();
  }

  getCoctailById() {
    this.coctailService.getCoctailById(this.id).subscribe(coctails => {
      if(coctails)this.coctail = coctails;
    })
  }
}
