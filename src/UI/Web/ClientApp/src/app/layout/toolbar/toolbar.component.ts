import { Component, EventEmitter, Input, Output, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarRef, SimpleSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Scheme } from 'src/app/app.config';

@Component({
  selector: 'toolbar',
  templateUrl: './toolbar.component.html',
  encapsulation: ViewEncapsulation.None
})
export class ToolbarComponent {
  @Input() scheme: Scheme = 'light';
  @Output() toggleSidenav = new EventEmitter<void>();
  @Output() toggleScheme = new EventEmitter<Scheme>();

  constructor(
    private _dialog: MatDialog,
    private _snackBar: MatSnackBar,
    private _router: Router) {
    }

  openSnackBar(message: string, action: string) : MatSnackBarRef<SimpleSnackBar> {
    return this._snackBar.open(message, action, {
      duration: 5000,
    });
  }
}
