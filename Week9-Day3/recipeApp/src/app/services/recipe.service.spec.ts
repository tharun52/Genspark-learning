import { HttpTestingController, provideHttpClientTesting } from "@angular/common/http/testing";
import { RecipeService } from "./recipe.service"
import { TestBed } from "@angular/core/testing";
import { provideHttpClient } from "@angular/common/http";
import mockRecipe from './mockRecipe.json';

describe('RecipieServiceTest', () => {
    let service: RecipeService;
    let httpMock: HttpTestingController;

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [],
            providers: [
                RecipeService,
                provideHttpClient(),
                provideHttpClientTesting()
            ]
        })
        service = TestBed.inject(RecipeService);
        httpMock = TestBed.inject(HttpTestingController);
    });

    afterEach(() => {
        httpMock.verify();
    })
    it('should be created', () => {
        expect(service).toBeTruthy();
    })
    it('should fetch all recipes from API', () => {
        const dummyResponse = { recipes: mockRecipe.recipes }; 

        service.getAllRecipes().subscribe((response) => {
            expect(response.recipes.length).toBe(30);
            expect(response.recipes).toEqual(mockRecipe.recipes);
        });

        const req = httpMock.expectOne('https://dummyjson.com/recipes');
        expect(req.request.method).toBe('GET');

        req.flush(dummyResponse); 
        });

})