import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Singleproduct } from './singleproduct';

describe('Singleproduct', () => {
  let component: Singleproduct;
  let fixture: ComponentFixture<Singleproduct>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Singleproduct]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Singleproduct);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
