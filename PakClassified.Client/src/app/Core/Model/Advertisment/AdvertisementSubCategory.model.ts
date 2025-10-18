import { AdvertisementCategory } from "./AdvertisementCategory.model";

export interface AdvertisementSubCategory {
    id: number;
    name?: string;
    categoryId : number;
    image?: string;
    description? : string;
    // categoryName? : string;
    category?: AdvertisementCategory;
}