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
  styleUrls: ['./drink.component.scss', '../style.css']
})

export class DrinkComponent implements OnInit {

  constructor(private coctailService: CoctailService,
              private _Activatedroute: ActivatedRoute,
              private router: Router,
              public accountService: AccountService,
              private adminService: AdminService,
              private toastr: ToastrService) { }

  id: number;
  coctail: Coctail;
  sub: Subscription;
  isFavourite: boolean = false;

  ngOnInit(): void {
    this.sub=this._Activatedroute.paramMap.subscribe(params => { 
      this.id = Number(params.get('id')); 
   });

   this.checkIsFavourite();
   this.getCoctailById();
  }

  getCoctailById() {
    this.coctailService.getCoctailById(this.id).subscribe(coctails => {
      if(coctails){
        console.log(coctails);
        this.coctail = coctails;
      } 
    })
  }

  acceptCoctail(){
    this.adminService.acceptCocktail(this.id).subscribe(() => {
      this.toastr.success('Dodano');
      this.router.navigateByUrl('/admin-panel');
    }, error => {
      this.toastr.error(error.error.errors[0]);
    })
  }

  rejectCoctail(){
    this.adminService.rejectCocktail(this.id).subscribe(() => {
      this.toastr.success('Usunieto');
      this.router.navigateByUrl('/admin-panel');
    }, error => {
      this.toastr.error(error.error.errors[0]);
    })
  }

  addToFavourite(){
    this.coctailService.addToFavourite(this.id).subscribe(() => {
      this.toastr.success('Added to favourite');
      this.checkIsFavourite();
    }, error => {
      this.toastr.error(error.error.errors[0]);
    })
  }

  deleteFromFavourited(){
    this.coctailService.deleteFromFavourited(this.id).subscribe(() => {
      this.toastr.success('Deleted to favourite');
      this.checkIsFavourite();
    }, error => {
      this.toastr.error(error.error.errors[0]);
    })
  }

  checkIsFavourite() {
    this.coctailService.isFavourite(this.id).subscribe(isFavourite => {
      this.isFavourite = isFavourite; 
    })
  }
}
