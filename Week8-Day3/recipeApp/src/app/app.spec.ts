import { TestBed } from '@angular/core/testing';
import { App } from './app';
import { RecipeService } from './services/recipe.service';

describe('App', () => {
  let recipeServiceSpy: jasmine.SpyObj<RecipeService>;
  beforeEach(async () => {
    const spy = jasmine.createSpyObj('RecipeService', ['getAllRecipes']);
    
    await TestBed.configureTestingModule({
      imports: [App],
      providers: [{ provide: RecipeService, useValue: spy }],
    }).compileComponents();
  });

  it('should create the app', () => {
    const fixture = TestBed.createComponent(App);
    const app = fixture.componentInstance;
    expect(app).toBeTruthy();
  });
});
