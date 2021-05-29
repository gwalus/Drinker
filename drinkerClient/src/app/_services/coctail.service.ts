import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';
import { getCoctailFiltersParams } from '../_helpers/coctailParamsHelper';
import { getPaginatedResult, getPaginationHeaders } from '../_helpers/paginationHelper';
import { Coctail } from '../_models/coctail';
import { CoctailParams } from '../_models/coctailParams';
import { PaginationParams } from '../_models/paginationParams';

@Injectable({
  providedIn: 'root'
})
export class CoctailService {
  baseUrl = environment.apiUrl + 'cocktails/';
  coctailParams: CoctailParams;

  constructor(private http: HttpClient, private toastr: ToastrService) { }

  searchCoctailByName(name: string) {
    return this.http.get<Coctail[]>(this.baseUrl + "byname/" + name);
  }

  getAll(name: string, ingredients: string[], coctailParams: CoctailParams) {
    let params = new HttpParams();

    params = params.append('name', name);

    for (let i = 0; i < ingredients.length; i++) {
      params = params.append('ingredients', ingredients[i]);
    }

    params = getPaginationHeaders(params, coctailParams);

    params = getCoctailFiltersParams(params, coctailParams);

    return getPaginatedResult<Coctail[]>(this.baseUrl, params, this.http);
  }

  getRandomCoctails(count: number = 1) {
    let params = new HttpParams();
    params = params.append('count', count.toString());

    return this.http.get<Coctail[]>(this.baseUrl + 'random', { params });
  }

  getCoctailCategories() {
    return this.http.get<string[]>(this.baseUrl + 'categories');
  }

  getCoctailGlasses() {
    return this.http.get<string[]>(this.baseUrl + 'glasses');
  }

  getCoctailById(id: number) {
    return this.http.get<Coctail>(this.baseUrl + 'byId/' + id);
  }

  getCoctailNames() {
    return this.http.get<string[]>(this.baseUrl + 'names');
  }

  getIngredientNames() {
    return this.http.get<string[]>(this.baseUrl + 'ingredientNames');
  }

  addToFavourite(id: number) {
    return this.http.post<string>(this.baseUrl + 'favourite?cocktailId=' + id, {});
  }

  getFavouritedCocktails(paginationParams: PaginationParams) {
    let params = new HttpParams();

    params = getPaginationHeaders(params, paginationParams);

    return getPaginatedResult<Coctail[]>(this.baseUrl + 'favourited', params, this.http);
  }

  deleteFromFavourited(cocktailId: number) {
    return this.http.delete(this.baseUrl + 'delete-from-favourited?cocktailId=' + cocktailId);
  }
}