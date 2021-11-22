import { Component, EventEmitter, Input, OnInit, Output, ViewEncapsulation } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Scheme } from 'src/app/app.config';
import { AuthService } from 'src/app/core/auth/services/auth.service';

@Component({
  selector: 'user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class UserProfileComponent implements OnInit {
  checkedDarkMode: boolean = false;

  public isAuthenticated: Observable<boolean>;
  public userName: Observable<string>;

  @Input() scheme: Scheme = 'light';
  @Output() toggleScheme = new EventEmitter<Scheme>();
  @Output() toggleDir = new EventEmitter<void>();
  
  constructor(private authService: AuthService) { }

  ngOnInit(): void {
    this.checkedDarkMode = this.scheme == 'light' ? false : true;
    this.isAuthenticated = this.authService.isAuthenticated();
    this.userName = this.authService.getUser().pipe(map(u => u && u.name));
  }

  updateScheme() {
    this.checkedDarkMode = !this.checkedDarkMode;

    const newScheme: Scheme = this.checkedDarkMode ? 'dark': 'light';
    
    this.toggleScheme.emit(newScheme);
  }
}

