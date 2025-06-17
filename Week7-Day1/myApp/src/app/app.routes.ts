import { Routes } from '@angular/router';
import { First } from './first/first';
import { CustomerDetails } from './customer-details/customer-details';

export const routes: Routes = [
    {path:'home', component:First},
    {path:'customer_details', component:CustomerDetails}
];
