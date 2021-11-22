import { Component, Inject, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
// import { HttpClient, HttpParams } from '@angular/common/http';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';

import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';

import { City } from './models/city';
import { CityService } from './city.service';
import { PagedList } from '../../shared/services/base.service';

@Component({
  selector: 'app-cities',
  templateUrl: './cities.component.html',
  encapsulation: ViewEncapsulation.None
})
export class CitiesComponent implements OnInit {
  public displayedColumns: string[] = ['id', 'name', 'lat', 'lon', 'countryName'];
  public cities: MatTableDataSource<City>;

  defaultPageIndex: number = 0;
  defaultPageSize: number = 10;
  public defaultSortColumn: string = "name";
  public defaultSortOrder: string = "asc";

  defaultFilterColumn: string = "name";
  filterQuery: string = null;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  filterTextChanged: Subject<string> = new Subject<string>();

  constructor(
    private cityService: CityService) {
  }

  ngOnInit() {
    this.loadData(null);
  }

  // debounce filter text changes
  onFilterTextChanged(filterText: string) {
    if (this.filterTextChanged.observers.length === 0) {
      this.filterTextChanged
        .pipe(debounceTime(1000), distinctUntilChanged())
        .subscribe(query => {
          this.loadData(query);
        });
    }
    this.filterTextChanged.next(filterText);
  }

  loadData(query: string = "") {
    var pageEvent = new PageEvent();
    pageEvent.pageIndex = this.defaultPageIndex;
    pageEvent.pageSize = this.defaultPageSize;
   /*  if (query) {
      this.filterQuery = query;
    } */
    this.filterQuery = query;
    this.getData(pageEvent);
  }

  getData(event: PageEvent) {

    var sortColumn = (this.sort)
      ? this.sort.active
      : this.defaultSortColumn;

    var sortOrder = (this.sort)
      ? this.sort.direction
      : this.defaultSortOrder;

    var filterColumn = (this.filterQuery)
      ? this.defaultFilterColumn
      : null;

    var filterQuery = (this.filterQuery)
      ? this.filterQuery
      : null;

      console.log("pageIndex: " + event.pageIndex)
    this.cityService.getData<PagedList<City>>(
      event.pageIndex,
      event.pageSize,
      sortColumn,
      sortOrder,
      filterQuery)
      .subscribe(result => {
        console.log(result)
        this.paginator.length = result.totalCount;
        this.paginator.pageIndex = result.page;
        this.paginator.pageSize = result.pageSize;
        this.cities = new MatTableDataSource<City>(result.source);
      }, error => console.error(error));
  }
}