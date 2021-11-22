import { BaseResponse } from "src/app/shared/models/base-response.model";
import { City } from "./city";

export interface CreateCityCommandResponse extends BaseResponse {
    city: City;
}