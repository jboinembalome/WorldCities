import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { ComponentModule  } from 'src/app/shared/components/component.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { CitiesComponent } from './cities.component';
import { CityEditComponent } from './city-edit/city-edit.component';
import { citiesRoutes } from './cities.routing';
import { MaterialModule } from 'src/app/shared/material/material.module';

@NgModule({
    declarations: [
      CitiesComponent,
      CityEditComponent
    ],
    imports     : [
        RouterModule.forChild(citiesRoutes),
        MaterialModule,
        ComponentModule,
        SharedModule
    ]
})
export class CitiesModule
{
}
