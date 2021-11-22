import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BaseService, PagedList } from '../../shared/services/base.service';
import { Observable } from 'rxjs';
import { IsDupeCityCommandResponse } from './models/isDupeCityCommandResponse';
import { CreateCityCommand } from './models/createCityCommand';
import { UpdateCityCommand } from './models/updateCityCommand';

@Injectable({
  providedIn: 'root',
})
export class CityService
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
        filterQuery: string
    ): Observable<PagedList> {
        var url = this.baseUrl + 'api/Cities';
        var params = new HttpParams()
            .set("page", page.toString())
            .set("pageSize", pageSize.toString())
            .set("sortColumn", sortColumn)
            .set("sortOrder", sortOrder);

        if (filterQuery) {
            params = params
                .set("filterQuery", filterQuery);
        }

        return this.http.get<PagedList>(url, { params });
    }

    get<City>(id: number): Observable<City> {
        var url = this.baseUrl + "api/Cities/" + id;
        return this.http.get<City>(url);
    }

    put<UpdateCityCommand>(item): Observable<UpdateCityCommand> {
        var url = this.baseUrl + "api/Cities/" + item.id;
        return this.http.put<UpdateCityCommand>(url, item);
    }

    post<CreateCityCommandResponse>(item: CreateCityCommand): Observable<CreateCityCommandResponse> {
        var url = this.baseUrl + "api/Cities";
        return this.http.post<CreateCityCommandResponse>(url, item);
    }

    getCountries<PagedList>(
        pageIndex: number,
        pageSize: number,
        sortColumn: string,
        sortOrder: string,
        filterColumn: string,
        filterQuery: string
    ): Observable<PagedList> {
        var url = this.baseUrl + 'api/Countries';
        var params = new HttpParams()
            .set("pageIndex", pageIndex.toString())
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

    isDupeCity(item): Observable<IsDupeCityCommandResponse> {
        var url = this.baseUrl + "api/Cities/IsDupeCity";
        return this.http.post<IsDupeCityCommandResponse>(url, item);
    }
}
