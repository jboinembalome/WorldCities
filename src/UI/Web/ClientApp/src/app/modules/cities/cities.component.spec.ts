import { async, ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterTestingModule } from '@angular/router/testing';
import { MaterialModule } from 'src/app/shared/material/material.module';
import { of } from 'rxjs';

import { CitiesComponent } from './cities.component';
import { City } from './models/city';
import { CityService } from './city.service';
import { PagedList } from 'src/app/shared/services/base.service';

describe('CitiesComponent', () => {
  let fixture: ComponentFixture<CitiesComponent>;
  let component: CitiesComponent;

  // async beforeEach(): TestBed initialization
  beforeEach(waitForAsync(() => {

    // Create a mock cityService object with a mock 'getData' method
    const cityService = jasmine.createSpyObj<CityService>(
      'CityService', ['getData']
    );

    // Configure the 'getData' spy method
    cityService.getData.and.returnValue(
      // return an Observable with some test data
      of<PagedList<City>>(<PagedList<City>>{
        source: [
          <City>{
            name: 'TestCity1',
            id: 1, lat: 1, lon: 1,
            countryId: 1, countryName: 'TestCountry1'
          },
          <City>{
            name: 'TestCity2',
            id: 2, lat: 1, lon: 1,
            countryId: 1, countryName: 'TestCountry1'
          },
          <City>{
            name: 'TestCity3',
            id: 3, lat: 1, lon: 1,
            countryId: 1, countryName: 'TestCountry1'
          }
        ],
        totalCount: 3,
        page: 0,
        pageSize: 10
      }));

    TestBed.configureTestingModule({
      declarations: [CitiesComponent],
      imports: [
        BrowserAnimationsModule,
        MaterialModule,
        RouterTestingModule
      ],
      providers: [
        {
          provide: CityService,
          useValue: cityService
        }
      ]
    })
      .compileComponents();
  }));

  // synchronous beforeEach(): fixtures and components setup
  beforeEach(() => {
    fixture = TestBed.createComponent(CitiesComponent);
    component = fixture.componentInstance;

    component.paginator = jasmine.createSpyObj(
      "MatPaginator", ["length", "pageIndex", "pageSize"]
    );

    fixture.detectChanges();
  });

  it('should display a "Cities" title', async(() => {
    let title = fixture.nativeElement
      .querySelector('h1');
    expect(title.textContent).toEqual('Cities');
  }));

  it('should contain a table with a list of one or more cities', async(() => {
    let table = fixture.nativeElement
      .querySelector('table.mat-table');
    let tableRows = table
      .querySelectorAll('tr.mat-row');
    expect(tableRows.length).toBeGreaterThan(0);
  }));
});
