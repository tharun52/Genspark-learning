import { Component } from '@angular/core';
import { UserService } from '../services/UserService';

@Component({
  selector: 'app-menu',
  imports: [],
  templateUrl: './menu.html',
  styleUrl: './menu.css'
})
export class Menu {
  username$: any;
  uname: string | null = "";

  constructor(private userService: UserService) {
    this.userService.username$.subscribe({
      next: (value) => {
        this.uname = value;
      },
      error: (err) => {
        alert(err);
      }
    })
  }
}
