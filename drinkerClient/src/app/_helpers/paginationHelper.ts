import { HttpClient, HttpParams } from "@angular/common/http";
import { PaginatedResult } from "../_models/pagination";
import { map } from 'rxjs/operators';
import { PaginationParams } from "../_models/paginationParams";

export function getPaginationHeaders(params: HttpParams, paginationParams: PaginationParams) {
    params = params.append('pageNumber', paginationParams.pageNumber.toString());
    params = params.append('pageSize', paginationParams.pageSize.toString());

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