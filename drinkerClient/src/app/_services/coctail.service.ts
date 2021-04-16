import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CoctailService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  searchCoctailByName(name: string) {
    return this.http.get(this.baseUrl + 'Coctail/search/byName/' + name);
  }
}
