import { HttpClient, HttpParams } from "@angular/common/http";
import { PaginatedResult } from "../_models/pagination";
import { map } from 'rxjs/operators';
import { CoctailParams } from "../_models/coctailParams";

export function getPaginationHeaders(coctailParams: CoctailParams) {
    let params = new HttpParams();

    params.append('pageNumber', coctailParams.pageNumber.toString());
    params.append('pageSize', coctailParams.pageSize.toString());
    params.append('category', coctailParams.category.toString());
    params.append('alcoholic', coctailParams.alcoholic.toString());
    params.append('glass', coctailParams.glass.toString());

    return params;
}

export function getPaginatedResult<T>(url: string, params: HttpParams, http: HttpClient) {
    const paginatedResult: PaginatedResult<T> = new PaginatedResult<T>();

    return http.get<T>(url, { observe: 'response', params }).pipe(
        map(response => {
            paginatedResult.result = response.body;
            if (response.headers.get('Pagination') !== null) {
                paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
            };
            return paginatedResult;
        })
    );
}