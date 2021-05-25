import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
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
    private router: Router,
    public accountService: AccountService) { }


  ngOnInit(): void {
    this.coctailService.getCoctailNames().subscribe(names => this.cocktailsAutoCompleteNames = names);

    this.filteredCocktailNames = this.searchControl.valueChanges
      .pipe(
        startWith(''),
        map(value => this._filter(value))
      );

    // this.coctailService.getRandomCoctails(8).subscribe(randomCoctails => {
    //   console.log(randomCoctails as Coctail);
    // })

    // this.coctailService.getCoctailCategories().subscribe(categories => {
    //   console.log(categories);
    // })

    // this.coctailService.getCoctailGlasses().subscribe(glasses => {
    //   console.log(glasses);
    // })
  }

  openLoginContent(loginContent: any) {
    this.modalService.open(loginContent, { scrollable: true });
  }


  openRegistraionContent(registrationContent: any) {
    this.modalService.open(registrationContent, { scrollable: true });
  }

  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();

    return this.cocktailsAutoCompleteNames.filter(option => option.toLowerCase().includes(filterValue));

  }

  onKeyDownEvent(event: any) {
    const name = this.searchControl.value;
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
      this.router.navigateByUrl('/search/' + name);
    });
  }

  logout() {
    this.accountService.logout();
  }
}
