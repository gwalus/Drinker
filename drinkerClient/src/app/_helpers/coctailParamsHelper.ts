import { HttpParams } from "@angular/common/http";
import { CoctailParams } from "../_models/coctailParams";

export function getCoctailFiltersParams(params: HttpParams, coctailParams: CoctailParams) {
    if (coctailParams.categories !== undefined) {
        for (let i = 0; i < coctailParams.categories.length; i++) {
            params = params.append('categories', coctailParams.categories[i]);
        }
    }

    if (coctailParams.alcoholicTypes !== undefined) {
        for (let i = 0; i < coctailParams.alcoholicTypes.length; i++) {
            params = params.append('alcoholicTypes', coctailParams.alcoholicTypes[i]);
        }
    }

    if (coctailParams.glasses !== undefined) {
        for (let i = 0; i < coctailParams.glasses.length; i++) {
            params = params.append('glasses', coctailParams.glasses[i]);
        }
    }

    return params;
}