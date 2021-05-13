import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { Coctail } from '../_models/coctail';
import { Pagination } from '../_models/pagination';
import { AccountService } from '../_services/account.service';
import { CoctailService } from '../_services/coctail.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  public searchedValue: string;
  coctail: Coctail;
  coctails: Coctail[] = [];
  allCoctails: Coctail[] = [];
  pagination: Pagination;
  paginationForAll: Pagination;

  searchControl = new FormControl();
  cocktailsAutoCompleteNames: string[] = [];
  filteredCocktailNames: Observable<string[]>;

  constructor(private coctailService: CoctailService,
    private modalService: NgbModal,
    private toastr: ToastrService,
    private router: Router,
    private route: ActivatedRoute,
    public accountService: AccountService) { }


  ngOnInit(): void {
    this.coctailService.getCoctailNames().subscribe(names => this.cocktailsAutoCompleteNames = names);

    this.filteredCocktailNames = this.searchControl.valueChanges
      .pipe(
        startWith(''),
        map(value => this._filter(value))
      );

    this.coctailService.getRandomCoctails(8).subscribe(randomCoctails => {
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


  openRegistraionContent(registrationContent: any) {
    this.modalService.open(registrationContent, { scrollable: true });

  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();

    return this.cocktailsAutoCompleteNames.filter(option => option.toLowerCase().includes(filterValue));

  }

  // testByIngredients() {
  //   const ingredients: string[] = [
  //     // 'Vodka'
  //   ];

  //   let coctailParams: CoctailParams = new CoctailParams();

  //   coctailParams.alcoholic = 'Alcoholic';
  //   coctailParams.category = 'Ordinary Drink';
  //   coctailParams.pageSize = 3;
  //   coctailParams.pageNumber = 3;

  //   this.coctailService.getCoctailsByIngredients(ingredients, coctailParams).subscribe(coctails => {
  //     this.coctails = coctails.result;
  //     this.pagination = coctails.pagination

  //     console.log(this.coctails)
  //   }, error => this.toastr.warning(error.error, 'Warning'))
  // }

  onKeyDownEvent(event: any) {
    const name = this.searchControl.value;
    this.router.navigateByUrl('/search/' + name);
  }

  logout() {
    this.accountService.logout();
  }
}
