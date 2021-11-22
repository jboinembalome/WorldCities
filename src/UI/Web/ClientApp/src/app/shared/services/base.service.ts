import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export abstract class BaseService {
    constructor(
        public http: HttpClient,
        public baseUrl: string
    ) {
    }

    abstract getData<PagedList>(
        pageIndex: number,
        pageSize: number,
        sortColumn: string,
        sortOrder: string,
        filterColumn: string,
        filterQuery: string): Observable<PagedList>;

    abstract get<T>(id: number): Observable<T>;
    abstract put<T>(item: any): Observable<T>;
    abstract post<T>(item: any): Observable<T>;
}

export interface PagedList<T> {
    source: T[];
    page: number;
    pageSize: number;
    totalCount: number;
    pageCount: number;
    sortColumn: string;
    sortOrder: string;
    filterColumn: string;
    filterQuery: string;
}
