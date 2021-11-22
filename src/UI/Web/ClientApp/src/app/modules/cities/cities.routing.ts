import { Route } from '@angular/router';
import { AuthGuard } from 'src/app/core/auth/guards/auth.guard';
import { CitiesComponent } from './cities.component';
import { CityEditComponent } from './city-edit/city-edit.component';

export const citiesRoutes: Route[] = [
    {
        path: '',
        data: {
            breadcrumb: 'cities'
        },
        component: CitiesComponent
    },
    {
        path: 'city/:id',
        data: {
            breadcrumb: 'update city'
        },
        component: CityEditComponent,
        canActivate: [AuthGuard]
    },
    {
        path: 'city',
        data: {
            breadcrumb: 'new city'
        },
        component: CityEditComponent,
        canActivate: [AuthGuard]
    },
];
