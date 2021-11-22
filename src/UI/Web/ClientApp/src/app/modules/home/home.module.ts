import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ComponentModule  } from 'src/app/shared/components/component.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { HomeComponent } from './home.component';
import { homeRoutes } from './home.routing';
import { MaterialModule } from 'src/app/shared/material/material.module';
import { IntroductionComponent } from './introduction/introduction.component';
import { TechnologyComponent } from './technology/technology.component';
import { AlertModule } from 'src/app/shared/components/alert/alert.module';


@NgModule({
    declarations: [
        HomeComponent,
        IntroductionComponent,
        TechnologyComponent
    ],
    imports     : [
        RouterModule.forChild(homeRoutes),
        AlertModule,
        ComponentModule,
        MaterialModule,
        SharedModule,

    ]
})
export class HomeModule
{
}
