import { HttpParams } from "@angular/common/http";

export function getPaginationHeaders(pageNumber: number, pageSize: number) {
    let params = new HttpParams();

    params.append('pageNumber', pageNumber.toString());
    params.append('pageSize', pageNumber.toString());

    return params;
}