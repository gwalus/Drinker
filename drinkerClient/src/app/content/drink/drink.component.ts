import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { Coctail } from 'src/app/_models/coctail';
import { AccountService } from 'src/app/_services/account.service';
import { AdminService } from 'src/app/_services/admin.service';
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
              private _router:Router,
              public accountService: AccountService,
              private adminService: AdminService,
              private toastr: ToastrService) { }

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

  acceptCoctail(){
    this.adminService.acceptCocktail(this.id).subscribe(() => {
      this.toastr.success('Dodano');
    }, error => {
      this.toastr.error(error.error.errors[0]);
    })
  }

  rejectCoctail(){
    this.adminService.rejectCocktail(this.id).subscribe(() => {
      this.toastr.success('Usunieto');
    }, error => {
      this.toastr.error(error.error.errors[0]);
    })
  }
}
