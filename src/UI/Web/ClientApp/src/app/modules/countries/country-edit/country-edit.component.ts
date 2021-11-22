import { Component, Inject, OnInit, ViewEncapsulation } from '@angular/core';
// import { HttpClient, HttpParams } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators, AbstractControl, AsyncValidatorFn } from '@angular/forms';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { BaseFormComponent } from '../../../shared/components/form/base.form.component';

import { Country } from '../models/country';
import { CountryService } from './../country.service';
import { UpdateCountryCommand } from '../models/updateCountryCommand';
import { CreateCountryCommand } from '../models/createCountryCommand';
import { IsDupeCountryCommand } from '../models/isDupeCountryCommand';
import { CreateCountryCommandResponse } from '../models/createCountryCommandResponse';

@Component({
  selector: 'app-country-edit',
  templateUrl: './country-edit.component.html',
  encapsulation: ViewEncapsulation.None
})
export class CountryEditComponent
  extends BaseFormComponent implements OnInit {

  // the view title
  title: string;

  // the form model
  form: FormGroup;

  // the city object to edit or create
  country: Country;

  // the city object id, as fetched from the active route:
  // It's NULL when we're adding a new country,
  // and not NULL when we're editing an existing one.
  id?: number;

  constructor(
    private fb: FormBuilder,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private countryService: CountryService) {
    super();
  }

  ngOnInit() {
    this.form = this.fb.group({
      name: ['',
        Validators.required,
        this.isDupeField("name")
      ],
      iso2: ['',
        [
          Validators.required,
          Validators.pattern(/^[a-zA-Z]{2}$/)
        ],
        this.isDupeField("iso2")
      ],
      iso3: ['',
        [
          Validators.required,
          Validators.pattern(/^[a-zA-Z]{3}$/)
        ],
        this.isDupeField("iso3")
      ]
    });

    this.loadData();
  }

  loadData() {

    // retrieve the ID from the 'id'
    this.id = +this.activatedRoute.snapshot.paramMap.get('id');
    if (this.id) {
      // EDIT MODE

      // fetch the country from the server
      this.countryService.get<Country>(this.id)
        .subscribe(result => {
          this.country = result;
          this.title = "Edit - " + this.country.name;

          // update the form with the country value
          this.form.patchValue(this.country);
        }, error => console.error(error));
    }
    else {
      // ADD NEW MODE

      this.title = "Create a new Country";
    }
  }

  onSubmit() {
    if (this.id) {
      let country = <UpdateCountryCommand>{};

      country.id = this.id;
      country.name = this.form.get("name").value;
      country.iso2 = this.form.get("iso2").value;
      country.iso3 = this.form.get("iso3").value;

      // EDIT mode
      this.countryService
        .put<UpdateCountryCommand>(country)
        .subscribe(result => {

          console.log("Country " + country.id + " has been updated.");

          // go back to cities view
          this.router.navigate(['/countries']);
        }, error => console.error(error));
    }
    else {

      let country = <CreateCountryCommand>{};

      country.id = this.id;
      country.name = this.form.get("name").value;
      country.iso2 = this.form.get("iso2").value;
      country.iso3 = this.form.get("iso3").value;

      // ADD NEW mode
      this.countryService
        .post<CreateCountryCommandResponse>(country)
        .subscribe(result => {

          console.log("Country " + result.country.id + " has been created.");

          // go back to cities view
          this.router.navigate(['/countries']);
        }, error => console.error(error));
    }
  }

  isDupeField(fieldName: string): AsyncValidatorFn {
    return (control: AbstractControl): Observable<{ [key: string]: any } | null> => {

      var countryId = (this.id) ? this.id : 0;

      let isDupeCountryCommand = <IsDupeCountryCommand>{};
      isDupeCountryCommand.countryId = countryId;
      isDupeCountryCommand.fieldName = fieldName;
      isDupeCountryCommand.fieldValue = control.value;

      return this.countryService.isDupeField(isDupeCountryCommand)
        .pipe(map(result => {
          return (result.isDupe ? { isDupeField: true } : null);
        }));
    }
  }
}
