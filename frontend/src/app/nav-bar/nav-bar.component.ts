import { Component, OnInit } from '@angular/core';
import {TokenServiceService} from '../../../serviceAngular/token-service.service'

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss'],
})
export class NavBarComponent  implements OnInit {
  searchQuery: string = '';

  isAdmin(): boolean {
    const userRole = this.tokenService.getUserRole();
    return userRole === 'admin';
  }

  constructor(private tokenService: TokenServiceService) { }

  ngOnInit() {}

  onSearch() {

  }
}
