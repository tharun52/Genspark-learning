import { ComponentFixture, TestBed } from '@angular/core/testing';
import MockRecipeData from '../services/mockRecipe.json';

import { Recipe } from './recipe';
import { RecipeModel } from '../models/RecipeModel';
import { By } from '@angular/platform-browser';

const mockRecipes: RecipeModel[] = MockRecipeData.recipes;

describe('Recipe', () => {
  let component: Recipe;
  let fixture: ComponentFixture<Recipe>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Recipe]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Recipe);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
   it('should display recipe details when input is set', () => {
    const recipe = mockRecipes[0];
    component.recipe = recipe;
    fixture.detectChanges();

    const img = fixture.debugElement.query(By.css('img')).nativeElement as HTMLImageElement;
    const title = fixture.debugElement.query(By.css('.card-title')).nativeElement;
    const cuisine = fixture.debugElement.query(By.css('.card-text')).nativeElement;
    const cookTime = fixture.debugElement.queryAll(By.css('.list-group-item'))[0].nativeElement;
    const ingredients = fixture.debugElement.queryAll(By.css('.list-group-item'))[1].nativeElement;

    expect(img.src).toContain(recipe.image);
    expect(title.textContent).toContain(recipe.name);
    expect(cuisine.textContent).toContain(recipe.cuisine);
    expect(cookTime.textContent).toContain(String(recipe.cookTimeMinutes));
    expect(ingredients.textContent).toContain(recipe.ingredients.join(', '));
  });

  it('should call the recipe setter using spy', () => {
    const recipe = mockRecipes[0];
    const spy = spyOnProperty(component, 'recipe', 'set').and.callThrough();
    component.recipe = recipe;
    expect(spy).toHaveBeenCalledWith(recipe);
  });
});
