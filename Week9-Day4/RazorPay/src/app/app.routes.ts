import { Routes } from '@angular/router';
import { PaymentForm } from './payment-form/payment-form';
import { Products } from './products/products';

export const routes: Routes = [
    {path:'', component:Products},
    {path:'payment', component:PaymentForm},    
];
