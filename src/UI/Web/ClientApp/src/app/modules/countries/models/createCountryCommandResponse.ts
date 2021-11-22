import { BaseResponse } from "src/app/shared/models/base-response.model";
import { Country } from "./country";

export interface CreateCountryCommandResponse extends BaseResponse {
    country: Country;
}