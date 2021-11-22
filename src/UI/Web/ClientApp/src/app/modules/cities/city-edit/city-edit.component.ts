import { Component, OnInit, OnDestroy, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl, Validators, AbstractControl, AsyncValidatorFn } from '@angular/forms';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { BaseFormComponent } from '../../../shared/components/form/base.form.component';

import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { City } from '../models/city';
import { Country } from '../../countries/models/country';
import { CityService } from '../city.service';
import { PagedList } from '../../../shared/services/base.service';
import { UpdateCityCommand } from '../models/updateCityCommand';
import { CreateCityCommand } from '../models/createCityCommand';
import { CreateCityCommandResponse } from '../models/createCityCommandResponse';
import { LogMessage } from 'src/app/shared/models/log-message.model';

@Component({
  selector: 'app-city-edit',
  templateUrl: './city-edit.component.html',
  encapsulation: ViewEncapsulation.None
})
export class CityEditComponent
  extends BaseFormComponent implements OnInit, OnDestroy {

  // the view title
  title: string;

  // the form model
  form: FormGroup;

  // the city object to edit or create
  city: City;

  // the list of log message
  logMessages: LogMessage[] = [];

  // the city object id, as fetched from the active route:
  // It's NULL when we're adding a new city,
  // and not NULL when we're editing an existing one.
  id?: number;

  // the countries observable for the select (using async pipe)
  countries: Observable<PagedList<Country>>;

  // Activity Log (for debugging purposes)
  activityLog: string = '';

  // Notifier subject (to avoid memory leaks)
  private destroySubject: Subject<boolean> = new Subject<boolean>();

  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private cityService: CityService) {
    super();
  }

  ngOnInit() {
    this.form = new FormGroup({
      name: new FormControl('', Validators.required),
      lat: new FormControl('', [
        Validators.required,
        Validators.pattern(/^[-]?[0-9]+(\.[0-9]{1,4})?$/)
      ]),
      lon: new FormControl('', [
        Validators.required,
        Validators.pattern(/^[-]?[0-9]+(\.[0-9]{1,4})?$/)
      ]),
      countryId: new FormControl('', Validators.required)
    }, null, this.isDupeCity());

    // react to form changes
    this.form.valueChanges
      .pipe(takeUntil(this.destroySubject))
      .subscribe(() => {
        if (!this.form.dirty) {
          this.log("Form Model has been loaded.");
        }
        else {
          this.log("Form was updated by the user.");
        }
      });

    // react to changes in the form.name control
    this.form.get("name")!.valueChanges
      .pipe(takeUntil(this.destroySubject))
      .subscribe(() => {
        if (!this.form.dirty) {
          this.log("Name has been loaded with initial values.");
        }
        else {
          this.log("Name was updated by the user.");
        }
      });

    this.loadData();
  }

  ngOnDestroy() {
    // emit a value with the takeUntil notifier
    this.destroySubject.next(true);
    // unsubscribe from the notifier itself
    this.destroySubject.unsubscribe();
  }

  log(str: string) {
    let activity: LogMessage = { date:new Date(), message:str }; 
    this.logMessages.push(activity);
  }

  clearLog() { 
    this.logMessages = [];
  }

  loadData() {

    // load countries
    this.loadCountries();

    // retrieve the ID from the 'id'
    this.id = +this.activatedRoute.snapshot.paramMap.get('id');
    if (this.id) {
      // EDIT MODE

      // fetch the city from the server
      this.cityService.get<City>(this.id).subscribe(result => {
        this.city = result;
        this.title = "Edit - " + this.city.name;

        // update the form with the city value
        this.form.patchValue(this.city);
      }, error => console.error(error));
    }
    else {
      // ADD NEW MODE

      this.title = "Create a new City";
    }
  }

  loadCountries() {
    // fetch all the countries from the server
    this.countries = this.cityService
      .getCountries<PagedList<Country>>(
        0,
        9999,
        "name",
        "asc",
        "",
        "",
      );
  }

  onSubmit() {

    if (this.id) {
      let city = <UpdateCityCommand>{};

      city.id = this.id;
      city.name = this.form.get("name").value;
      city.lat = +this.form.get("lat").value;
      city.lon = +this.form.get("lon").value;
      city.countryId = +this.form.get("countryId").value;

      // EDIT mode
      this.cityService
        .put<UpdateCityCommand>(city)
        .subscribe(() => {

          console.log("City " + city.id + " has been updated.");

          // go back to cities view
          this.router.navigate(['/cities']);
        }, error => console.error(error));
    }
    else {
      let city = <CreateCityCommand>{};

      city.name = this.form.get("name").value;
      city.lat = +this.form.get("lat").value;
      city.lon = +this.form.get("lon").value;
      city.countryId = +this.form.get("countryId").value;

      // ADD NEW mode
      this.cityService
        .post<CreateCityCommandResponse>(city)
        .subscribe(result => {

          console.log("City " + result.city.id + " has been created.");

          // go back to cities view
          this.router.navigate(['/cities']);
        }, error => console.error(error));
    }
  }

  isDupeCity(): AsyncValidatorFn {
    return (control: AbstractControl): Observable<{ [key: string]: any } | null> => {
      var city = <City>{};
      city.id = (this.id) ? this.id : 0;
      city.name = this.form.get("name").value;
      city.lat = +this.form.get("lat").value;
      city.lon = +this.form.get("lon").value;
      city.countryId = +this.form.get("countryId").value;

      return this.cityService.isDupeCity(city)
        .pipe(map(result => {
          return (result.isDupe ? { isDupeCity: true } : null);
        }));
    }
  }
}
