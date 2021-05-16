import { PaginationParams } from "./paginationParams";

export class CoctailParams extends PaginationParams {
    categories: string[];
    alcoholicTypes: string[];
    glasses: string[];
}