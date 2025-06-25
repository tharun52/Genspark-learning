import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Menu } from './menu';
import { UserService } from '../services/UserService';
import { of, throwError } from 'rxjs';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

const mockActivatedRoute = {
  snapshot:{
    paramMap:{
      get:(key:string)=>{
        if(key=='id') return '1';
        return null;
      }
    }
  }
}

describe('Menu Component (Standalone)', () => {
  let component: Menu;
  let fixture: ComponentFixture<Menu>;
  let userServiceSpy: jasmine.SpyObj<UserService>;

  beforeEach(async () => {
    const spy = jasmine.createSpyObj('UserService', ['username$'], { username$: of('TestUser') });

    await TestBed.configureTestingModule({
      imports: [Menu], // standalone component
      providers: [
        { provide: UserService, useValue: spy },
        {provide:ActivatedRoute, useValue:spy}
      ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA], // In case RouterLink or others are not mocked
    }).compileComponents();

    fixture = TestBed.createComponent(Menu);
    component = fixture.componentInstance;
    userServiceSpy = TestBed.inject(UserService) as jasmine.SpyObj<UserService>;
    fixture.detectChanges();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should set usrname from UserService observable', () => {
    expect(component.usrname).toBe('TestUser');
  });

  it('should render username in template', () => {
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.textContent).toContain('TestUser');
  });

  it('should handle error in subscription', () => {
    const errorSpy = jasmine.createSpyObj('UserService', [], {
      username$: throwError(() => new Error('User fetch failed')),
    });

    spyOn(window, 'alert');
    TestBed.overrideProvider(UserService, { useValue: errorSpy });

    const errorFixture = TestBed.createComponent(Menu);
    errorFixture.detectChanges();

    expect(window.alert).toHaveBeenCalledWith(new Error('User fetch failed'));
  });
});
