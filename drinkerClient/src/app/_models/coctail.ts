import { Ingredient } from "./ingredient";

export interface Coctail {
    id: number;
    name: string;
    category: string;
    alcoholic: string;
    glass: string;
    instructions: string;
    photoUrl: string;
    dateModified: Date,
    isAccepted: boolean,
    ingradients: Ingredient[]
}