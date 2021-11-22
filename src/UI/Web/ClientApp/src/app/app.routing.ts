import { Route } from '@angular/router';

import { AuthGuard } from 'src/app/core/auth/guards/auth.guard';
import { LayoutComponent } from './layout/layout.component';

export const appRoutes: Route[] = [
    // Redirect empty path to '/home'
    { path: '', pathMatch: 'full', redirectTo: 'home' },

    // Auth routes for guests
    {
        path: '',
        //canActivate: [NoAuthGuard],
        //canActivateChild: [NoAuthGuard],
        component: LayoutComponent,
        children: [
            { path: 'home', loadChildren: () => import('./modules/home/home.module').then(m => m.HomeModule) },
            { path: 'countries', loadChildren: () => import('./modules/countries/countries.module').then(m => m.CountriesModule) },
            { path: 'cities', loadChildren: () => import('./modules/cities/cities.module').then(m => m.CitiesModule) },
            { path: 'auth', loadChildren: () => import('./core/auth/auth.module').then(m => m.AuthModule) },
        ]
    },

    // Auth routes for authenticated users
    {
        path: '',
        //canActivate: [AuthGuard],
        //canActivateChild: [AuthGuard],
        component: LayoutComponent,
        children: [
        ]
    }
];
