import { HttpParams } from "@angular/common/http";
import { CoctailParams } from "../_models/coctailParams";

export function getCoctailFiltersParams(params: HttpParams, coctailParams: CoctailParams) {
    if (coctailParams.category !== undefined) {
        params = params.append('category', coctailParams.category);
    }

    if (coctailParams.alcoholic !== undefined) {
        params = params.append('alcoholic', coctailParams.alcoholic);
    }

    if (coctailParams.glass !== undefined) {
        params = params.append('glass', coctailParams.glass);
    }

    return params;
}