import { Component, Input, OnInit, ViewEncapsulation } from '@angular/core';

@Component({
    selector     : 'blouppy-title',
    templateUrl  : './title.component.html',
    encapsulation: ViewEncapsulation.None
})
export class TitleComponent implements OnInit
{
    @Input() text: string = '';

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
}
