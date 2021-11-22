import { Inject, Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { merge } from 'lodash-es';
import { APP_CONFIG } from './config.constant';

@Injectable({
    providedIn: 'root'
})
export class ConfigService {
    private _config: BehaviorSubject<any>;

    constructor(@Inject(APP_CONFIG) config: any) {
        this._config = new BehaviorSubject(config);
    }

    set config(value: any) {
        // Merge the new config over to the current config
        const config = merge({}, this._config.getValue(), value);

        // Execute the observable
        this._config.next(config);
    }

    get config(): Observable<any> {
        return this._config.asObservable();
    }

    /**
     * Resets the config to the default
     */
    reset(): void {
        // Set the config
        this._config.next(this.config);
    }
}
