import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BaseService, PagedList } from '../../shared/services/base.service';
import { Observable } from 'rxjs';
import { IsDupeCountryCommandResponse } from './models/isDupeCountryCommandResponse';
import { IsDupeCountryCommand } from './models/isDupeCountryCommand';
import { CreateCountryCommand } from './models/createCountryCommand';

@Injectable({
  providedIn: 'root',
})
export class CountryService
    extends BaseService {
    constructor(
        http: HttpClient,
        @Inject('BASE_URL') baseUrl: string) {
        super(http, baseUrl);
    }

    getData<PagedList>(
        page: number,
        pageSize: number,
        sortColumn: string,
        sortOrder: string,
        filterColumn: string,
        filterQuery: string
    ): Observable<PagedList> {
        var url = this.baseUrl + 'api/Countries';
        var params = new HttpParams()
            .set("page", page.toString())
            .set("pageSize", pageSize.toString())
            .set("sortColumn", sortColumn)
            .set("sortOrder", sortOrder);

        if (filterQuery) {
            params = params
                .set("filterColumn", filterColumn)
                .set("filterQuery", filterQuery);
        }

        return this.http.get<PagedList>(url, { params });
    }

    get<Country>(id: number): Observable<Country> {
        var url = this.baseUrl + "api/Countries/" + id;
        return this.http.get<Country>(url);
    }

    put<UpdateCountryCommand>(item): Observable<UpdateCountryCommand> {
        var url = this.baseUrl + "api/Countries/" + item.id;
        return this.http.put<UpdateCountryCommand>(url, item);
    }

    post<CreateCountryCommandResponse>(item: CreateCountryCommand): Observable<CreateCountryCommandResponse> {
        var url = this.baseUrl + "api/Countries";
        return this.http.post<CreateCountryCommandResponse>(url, item);
    }

    isDupeField(item: IsDupeCountryCommand): Observable<IsDupeCountryCommandResponse> {  
        var url = this.baseUrl + "api/Countries/IsDupeField";
        return this.http.post<IsDupeCountryCommandResponse>(url, item);
    }
}
