import { Component } from '@angular/core';
// import { RouterOutlet } from '@angular/router';
import { Login } from "./login/login";
import { Menu } from "./menu/menu";

@Component({
  selector: 'app-root',
  imports: [Login, Menu],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected title = 'welcomePage';
}
