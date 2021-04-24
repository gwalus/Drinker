import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Coctail } from '../_models/coctail';
import { CoctailParams } from '../_models/coctailParams';

@Injectable({
  providedIn: 'root'
})
export class CoctailService {
  baseUrl = environment.apiUrl;
  coctailParams: CoctailParams;

  constructor(private http: HttpClient) { }

  searchCoctailByName(name: string) {
    return this.http.get<Coctail>(this.baseUrl + 'Coctail/search/byName/' + name);
  }

  getCoctailsByIngredients(ingredients: string[]) {
    let params = new HttpParams();

    for (let i = 0; i < ingredients.length; i++) {
      params = params.append('ingredients', ingredients[i]);
    }
    return this.http.get<Coctail[]>(this.baseUrl + 'Coctail/search/byIngredients', { params: params });
  }
}
