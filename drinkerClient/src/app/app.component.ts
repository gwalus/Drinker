import { Component, OnInit } from '@angular/core';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';
import { CoctailService } from './_services/coctail.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Drinker';

  constructor(private accountService: AccountService, private cocktail: CoctailService) {
  }

  ngOnInit(): void {
    this.setCurrentUser();

    // this.cocktail.isFavourite(11020).subscribe(res => console.log(res));
  }

  setCurrentUser() {
    const user: User = JSON.parse(localStorage.getItem('user'));
    if (user) {
      this.accountService.setCurrentUser(user);
    }
  }
}
