import { Routes } from '@angular/router';
import { First } from './first/first';
import { CustomerDetails } from './customer-details/customer-details';
import { Home } from './home/home';
import { Login } from './login/login';

import { Products } from './products/products';
import { Profile } from './profile/profile';
import { AuthGuard } from './auth-guard-guard';
import { UserList } from './user-list/user-list';

export const routes: Routes = [
    {path:'landing',component:First},
    {path:'login',component:Login},
    {path:'products',component:Products},
    {path:'home/:uname',component:Home,children:
        [
            {path:'products',component:Products},
            {path:'customer_details',component:CustomerDetails}
        ]
    },
    {path:'profile', component:Profile, canActivate:[AuthGuard]},
    {path:'users',component:UserList}
]
