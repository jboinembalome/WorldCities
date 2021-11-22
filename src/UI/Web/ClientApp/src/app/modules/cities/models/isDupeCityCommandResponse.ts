import { BaseResponse } from "src/app/shared/models/base-response.model";

export interface IsDupeCityCommandResponse extends BaseResponse {
    isDupe: boolean;
}