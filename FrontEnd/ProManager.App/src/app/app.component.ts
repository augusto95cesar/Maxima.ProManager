import { Component, OnInit } from '@angular/core';
import { RouterOutlet, Router, NavigationEnd } from '@angular/router';
import { filter } from 'rxjs';
import { SidebarComponent } from './sidebar/sidebar.component'; // Importe o componente Sidebar
import { NgIf } from '@angular/common';


@Component({
  selector: 'app-root',
  imports: [RouterOutlet, SidebarComponent, NgIf],
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  
  isLoginRoute: boolean | any;

  constructor(private router: Router) { }

  ngOnInit() {
    this.router.events
      .pipe(
        filter(event => event instanceof NavigationEnd)
      )
      .subscribe(() => {
        this.isLoginRoute = this.router.url !== '/';
      });
  }
  title = 'ProManager.App';
}
