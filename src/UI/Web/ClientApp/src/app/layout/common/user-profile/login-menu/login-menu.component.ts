import { Component, Input, OnInit, ViewEncapsulation } from '@angular/core';

@Component({
  selector: 'app-login-menu',
  templateUrl: './login-menu.component.html',
  styleUrls: ['./login-menu.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class LoginMenuComponent implements OnInit {
  @Input() isAuthenticated: boolean;

  constructor() { }

  ngOnInit() {
  }
}
