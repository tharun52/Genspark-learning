import { Component, Input, signal, effect } from '@angular/core';
import { RecipeModel } from '../models/RecipeModel';

@Component({
  selector: 'app-recipe',
  standalone: true,
  templateUrl: './recipe.html',
  styleUrl: './recipe.css'
})
export class Recipe {
  private readonly recipeSignal = signal<RecipeModel | null>(null);

  @Input()
  set recipe(value: RecipeModel | null) {
    this.recipeSignal.set(value);
  }
  get recipe() {
    return this.recipeSignal();
  }

  constructor() {
  }
}
