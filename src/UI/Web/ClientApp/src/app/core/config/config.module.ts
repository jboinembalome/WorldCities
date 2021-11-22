import { ModuleWithProviders, NgModule } from '@angular/core';
import { APP_CONFIG } from './config.constant';

@NgModule()
export class ConfigModule {

    constructor() { }

    /**
     * forRoot method for setting user configuration
     *
     * @param config
     */
    static forRoot(config: any): ModuleWithProviders<ConfigModule> {
        return {
            ngModule: ConfigModule,
            providers: [
                {
                    provide: APP_CONFIG,
                    useValue: config
                }
            ]
        };
    }
}
