import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SidenavComponent } from './sidenav.component';
import { ToolbarModule } from '../toolbar/toolbar.module';
import { MaterialModule } from 'src/app/shared/material/material.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { LoadingBarModule } from 'src/app/shared/components/loading-bar';


@NgModule({
    declarations: [
        SidenavComponent
    ],
    imports     : [
        RouterModule,
        LoadingBarModule,
        ToolbarModule,
        SharedModule,
        MaterialModule
    ],
    exports     : [
        SidenavComponent,
    ]
})
export class SidenavModule
{
}
