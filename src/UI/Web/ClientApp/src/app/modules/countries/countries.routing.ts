import { Route } from '@angular/router';
import { AuthGuard } from 'src/app/core/auth/guards/auth.guard';
import { CountriesComponent } from './countries.component';
import { CountryEditComponent } from './country-edit/country-edit.component';

export const countriesRoutes: Route[] = [
    {
        path: '',
        component: CountriesComponent
    },
    {
        path: 'country/:id',
        component: CountryEditComponent,
        canActivate: [AuthGuard]
    },
    {
        path: 'country',
        component: CountryEditComponent,
        canActivate: [AuthGuard]
    },
];
