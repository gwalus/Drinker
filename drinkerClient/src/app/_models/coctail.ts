import { Ingredient } from "./ingredient";

export interface Coctail {
    name: string;
    category: string;
    alcoholic: string;
    glass: string;
    instructions: string;
    photoUrl: string;
    dateModified: Date,
    ingredients: Ingredient[]
}