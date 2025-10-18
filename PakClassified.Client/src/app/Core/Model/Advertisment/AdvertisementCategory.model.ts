import { AdvertisementSubCategory } from "./AdvertisementSubCategory.model";

export interface AdvertisementCategory {
    id: number;
    name: string;
    image: string;
    // SubCategory?: AdvertisementSubCategory | null;
    description?: string;
}