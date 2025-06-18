import { Routes } from '@angular/router';
import { Users } from './users/users';
import { AddUser } from './add-user/add-user';
import { Dashboard } from './dashboard/dashboard';

export const routes: Routes = [
    {path:'users', component:Users},
    {path:'add', component:AddUser},
    {path:'dashboard', component:Dashboard}
];
