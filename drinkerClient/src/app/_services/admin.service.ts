import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { getPaginatedResult, getPaginationHeaders } from '../_helpers/paginationHelper';
import { Coctail } from '../_models/coctail';
import { PaginationParams } from '../_models/paginationParams';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  baseUrl = environment.apiUrl + 'admin/';

  constructor(private http: HttpClient) { }

  getCocktailsToAccept(paginationParams: PaginationParams) {
    let params = new HttpParams();

    params = getPaginationHeaders(params, paginationParams);

    return getPaginatedResult<Coctail[]>(this.baseUrl + 'for-approval', params, this.http);
  }

  acceptCocktail(id: number) {
    return this.http.put(this.baseUrl + 'acceptcoctail?id=' + id, {});
  }

  rejectCocktail(id: number) {
    return this.http.delete(this.baseUrl + 'rejectcoctail?id=' + id);
  }
}
