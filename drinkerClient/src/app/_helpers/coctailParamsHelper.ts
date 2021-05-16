import { HttpParams } from "@angular/common/http";
import { CoctailParams } from "../_models/coctailParams";

export function getCoctailFiltersParams(params: HttpParams, coctailParams: CoctailParams) {
    if (coctailParams.categories !== undefined) {
        params = params.append('category', coctailParams.categories);
    }

    if (coctailParams.alcoholicTypes !== undefined) {
        params = params.append('alcoholic', coctailParams.alcoholicTypes);
    }

    if (coctailParams.glasses !== undefined) {
        params = params.append('glass', coctailParams.glasses);
    }

    return params;
}