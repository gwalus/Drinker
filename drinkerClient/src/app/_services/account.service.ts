import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import jwtDecode from 'jwt-decode';
import { ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { AuthUser } from '../_models/registerUser';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.apiUrl;

  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) { }

  register(model: AuthUser) {
    return this.http.post(this.baseUrl + 'identity/register', model).pipe(
      map(response => {
        const user = response as User;
        if (user) {
          this.setCurrentUser(user);
        }
      })
    )
  }

  login(model: AuthUser) {
    return this.http.post(this.baseUrl + 'identity/login', model).pipe(
      map(response => {
        let user = response as User;
        if (user) {
          this.setCurrentUser(user);
        }
      })
    )
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }

  setCurrentUser(user: User) {
    let decodedToken = this.getDecodedToken(user.token);
    user.email = decodedToken.email;

    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
  }

  getDecodedToken(token: string): User {
    return jwtDecode(token);
  }
}
