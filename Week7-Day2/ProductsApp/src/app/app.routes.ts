import { Routes } from '@angular/router';
import { About } from './about/about';
import { Products } from './products/products';
import { Login } from './login/login';
import { AuthGuard } from './auth-products-guard';
import { Singleproduct } from './singleproduct/singleproduct';

export const routes: Routes = [
    {path:'login', component:Login},
    {path:'about', component:About},
    {path:'products', component:Products,canActivate:[AuthGuard]},
    {path:'product/:id', component:Singleproduct}
];
