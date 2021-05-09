import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { RegisterUser } from '../_models/registerUser';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.apiUrl;

  private currentUserSource = new ReplaySubject(1);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) { }

  register(model: RegisterUser) {
    let registerUser: RegisterUser = {
      username: 'user2',
      password: 'Pa$$w0rd',
      confirmPassword: 'Pa$$w0rd'
    };

    model = registerUser;

    return this.http.post(this.baseUrl + 'identity/register', model).pipe(
      map(response => {
        const user = response as User;
        if (user) {
          this.setCurrentUser(user);
        }
      })
    )
  }

  setCurrentUser(user: User) {
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
  }
}
