import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { ComponentModule  } from 'src/app/shared/components/component.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { CountriesComponent } from './countries.component';
import { CountryEditComponent } from './country-edit/country-edit.component';
import { countriesRoutes } from './countries.routing';
import { MaterialModule } from 'src/app/shared/material/material.module';

@NgModule({
    declarations: [
      CountriesComponent,
      CountryEditComponent
    ],
    imports     : [
        RouterModule.forChild(countriesRoutes),
        MaterialModule,
        ComponentModule,
        SharedModule
    ]
})
export class CountriesModule
{
}
