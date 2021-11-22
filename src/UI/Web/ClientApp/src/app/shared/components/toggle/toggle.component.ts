import { Component, Input, OnInit, ViewEncapsulation } from '@angular/core';

@Component({
    selector     : 'blouppy-toggle',
    templateUrl  : './toggle.component.html',
    encapsulation: ViewEncapsulation.None
})
export class ToggleComponent implements OnInit
{
    @Input() checked: boolean = false;

    /**
     * Constructor
     */
    constructor()
    {
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Accessors
    // -----------------------------------------------------------------------------------------------------


    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On init
     */
    ngOnInit(): void
    {      
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------
    updateChecked(): void
    {
        this.checked = !this.checked;
    }
}
