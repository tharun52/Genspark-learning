import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { RecipesComponent } from "./recipies/recipies";

@Component({
  selector: 'app-root',
  imports: [RecipesComponent],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected title = 'recipeApp';
}
