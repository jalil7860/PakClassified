import { City } from "../location/city.model";
import { CityArea } from "../location/cityArea.model";
import { Role } from "../User/Role.model";
import { User } from "../User/User.model";
// import { Role } from "../Role/Role.model";
import { AdvertisementImage } from "./AdvertisementImage.model";
import { AdvertisementStatus } from "./AdvertisementStatus.model";
import { AdvertisementSubCategory } from "./AdvertisementSubCategory.model";
import { AdvertisementTag } from "./AdvertisementTag.model";
import { AdvertisementType } from "./AdvertisementType.model";

export interface AdvertisementModel {
    id: number;
    subCategoryId: number;
    subCategory?: AdvertisementSubCategory;
 
    statusId: number;
    status?: AdvertisementStatus;
 
    typeId: number;
    type?: AdvertisementType;
 
    postedById: number;
    postedBy?: User;
 
    cityAreaId: number;
    cityArea?: CityArea;
    city?: City;
 
    name: string;
    price: number;
    description: string;
    features: string
    hits: number;
    endsOn: string;
    startsOn: string;
    createdDate: string;
    image: string;
}