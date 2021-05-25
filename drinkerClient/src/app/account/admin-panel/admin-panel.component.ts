import { Component, OnInit } from '@angular/core';
import { Coctail } from 'src/app/_models/coctail';
import { PaginationParams } from 'src/app/_models/paginationParams';
import { AdminService } from 'src/app/_services/admin.service';

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.scss', '../style.css']
})
export class AdminPanelComponent implements OnInit {

  constructor(private adminService: AdminService) { }

  ngOnInit(): void {
    this.getCoctails();
  }

  paginationParams: PaginationParams = new PaginationParams();
  viewCoctails: Coctail[];
  
  getCoctails(){
    this.paginationParams.pageNumber = 1;
    this.paginationParams.pageSize = 12;
    this.adminService.getCocktailsToAccept(this.paginationParams).subscribe(coctails => {
      this.viewCoctails = coctails.result;
    })
  }
}
