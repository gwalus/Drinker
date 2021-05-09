import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { Coctail } from '../_models/coctail';
import { CoctailParams } from '../_models/coctailParams';
import { Pagination } from '../_models/pagination';
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

  constructor(private coctailService: CoctailService,
    private modalService: NgbModal,
    private toastr: ToastrService,
    private router: Router,
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.coctailService.getRandomCoctails(9).subscribe(randomCoctails => {
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

  testByIngredients() {
    const ingredients: string[] = [
      // 'Vodka'
    ];

    let coctailParams: CoctailParams = new CoctailParams();

    coctailParams.alcoholic = 'Alcoholic';
    coctailParams.category = 'Ordinary Drink';
    coctailParams.pageSize = 3;
    coctailParams.pageNumber = 3;

    this.coctailService.getCoctailsByIngredients(ingredients, coctailParams).subscribe(coctails => {
      this.coctails = coctails.result;
      this.pagination = coctails.pagination

      console.log(this.coctails)
    }, error => this.toastr.warning(error.error, 'Warning'))
  }

  onKeyDownEvent(event: any) {
    if (this.searchedValue)
      this.router.navigate(['/search/' + this.searchedValue]);
  }

  // register() {
  //   this.accountService.register().subscribe(() => {
  //     console.log('success');
  //   }, error => {
  //     this.toastr.error(error.error.errors[0]);
  //   })
  // }
}
