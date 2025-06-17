import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { authProductsGuard } from './auth-products-guard';

describe('authProductsGuard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => authProductsGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
