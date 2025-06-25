import { Component, OnInit, signal } from '@angular/core';
import { RecipeService } from '../services/recipe.service';
import { Recipe } from '../recipe/recipe';
import { RecipeModel } from '../models/RecipeModel';

@Component({
  selector: 'app-recipies',
  imports: [Recipe],
  templateUrl: './recipies.html',
  styleUrl: './recipies.css',
  standalone: true
})
export class RecipesComponent implements OnInit {
  recipes:RecipeModel[]|undefined = undefined;

  constructor(private recipeService: RecipeService) {}

  ngOnInit() {
    this.recipeService.getAllRecipes().subscribe(
      {
        next:(data:any) => {
          this.recipes = data.recipes as RecipeModel[];
        },
        error:(err)=>{},
        complete:()=>{}
      }
    ) }
}