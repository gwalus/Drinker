import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { getPaginatedResult, getPaginationHeaders } from '../_helpers/paginationHelper';
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

  getCoctailsByIngredients(ingredients: string[], coctailParams: CoctailParams) {
    let params = new HttpParams();

    params = getPaginationHeaders(coctailParams);

    for (let i = 0; i < ingredients.length; i++) {
      params = params.append('ingredients', ingredients[i]);
    }

    return getPaginatedResult<Coctail[]>(this.baseUrl + 'Coctail/search/byIngredients/', params, this.http);
  }
}
