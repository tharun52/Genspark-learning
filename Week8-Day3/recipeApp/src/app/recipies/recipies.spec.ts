import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RecipesComponent } from './recipies';
import { RecipeService } from '../services/recipe.service';
import { of, throwError } from 'rxjs';
import { Component, CUSTOM_ELEMENTS_SCHEMA, Input } from '@angular/core';
import { RecipeModel } from '../models/RecipeModel';
import MockRecipeData from '../services/mockRecipe.json';
import { Recipe } from '../recipe/recipe';

// Stub for <app-recipe>
@Component({
  selector: 'app-recipe',
  template: '',
  standalone: true
})
class MockRecipeComponent {
  @Input() recipe!: RecipeModel;
}

describe('RecipesComponent', () => {
  let component: RecipesComponent;
  let fixture: ComponentFixture<RecipesComponent>;
  let recipeServiceSpy: jasmine.SpyObj<RecipeService>;
    

  const mockRecipes: RecipeModel[] = MockRecipeData.recipes;

  beforeEach(async () => {
    const spy = jasmine.createSpyObj('RecipeService', ['getAllRecipes']);

    await TestBed.configureTestingModule({
      imports: [MockRecipeComponent, RecipesComponent],
      providers: [{ provide: RecipeService, useValue: spy }],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    }).compileComponents();

    recipeServiceSpy = TestBed.inject(RecipeService) as jasmine.SpyObj<RecipeService>;
    fixture = TestBed.createComponent(RecipesComponent);
    component = fixture.componentInstance;


  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should load recipes on init', () => {
    recipeServiceSpy.getAllRecipes.and.returnValue(of({ recipes: mockRecipes }));

    fixture.detectChanges();

    expect(component.recipes).toEqual(mockRecipes);
    expect(recipeServiceSpy.getAllRecipes).toHaveBeenCalledTimes(1);
  });
  it('should handle error from getAllRecipes', () => {
    recipeServiceSpy.getAllRecipes.and.returnValue(throwError(() => new Error('API error')));

    fixture.detectChanges();

    expect(component.recipes).toBeUndefined();
  });
});
