import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Menu } from "./menu/menu";

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  styleUrl: './app.css',
  imports: [RouterOutlet, Menu]
})
export class App {
  protected title = 'myApp';
}
