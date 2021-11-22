import { BaseResponse } from "src/app/shared/models/base-response.model";

export interface IsDupeCountryCommandResponse extends BaseResponse {
    isDupe: boolean;
}