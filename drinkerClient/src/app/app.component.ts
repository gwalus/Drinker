import { Component, OnInit } from '@angular/core';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';
import { AdminService } from './_services/admin.service';
import { CoctailService } from './_services/coctail.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Drinker';

  constructor(private accountService: AccountService, private admin: AdminService) {
  }

  ngOnInit(): void {
    this.setCurrentUser();

    this.admin.acceptCocktail(11000).subscribe(() => console.log('Dodano'), () => console.log('Nie dodano'));
  }

  setCurrentUser() {
    const user: User = JSON.parse(localStorage.getItem('user'));
    if (user) {
      this.accountService.setCurrentUser(user);
    }
  }
}
