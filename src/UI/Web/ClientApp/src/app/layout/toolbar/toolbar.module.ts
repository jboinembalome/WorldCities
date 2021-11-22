import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ToolbarComponent } from './toolbar.component';
import { UserProfileModule } from '../common/user-profile/user-profile.module';
import { MaterialModule } from 'src/app/shared/material/material.module';
import { SharedModule } from 'src/app/shared/shared.module';


@NgModule({
    declarations: [
        ToolbarComponent
    ],
    imports     : [
        RouterModule,
        UserProfileModule,
        SharedModule,
        MaterialModule
    ],
    exports     : [
        ToolbarComponent,
    ]
})
export class ToolbarModule
{
}
