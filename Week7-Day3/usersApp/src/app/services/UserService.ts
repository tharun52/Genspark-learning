import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { BehaviorSubject, Observable } from "rxjs";
import { UserModel } from "../models/UserModel";
import { map, tap } from 'rxjs/operators';
import { FilterModel } from "../models/FilterModel";

@Injectable()
export class UserService{
    private http = inject(HttpClient);
    private usersSubject = new BehaviorSubject<UserModel[]>([]);
    users$ = this.usersSubject.asObservable();

    constructor(){
        this.loadUsers();
    }
    loadUsers(filter?: FilterModel): void {
        this.http.get<any>('https://dummyjson.com/users').pipe(
            map(res => res.users as UserModel[]),
            map(users => {
            if (!filter) return users;

            return users.filter(user => {
                const genderMatch = filter.gender ? user.gender === filter.gender : true;
                const ageMatch = filter.age ? user.age === filter.age : true;
                const roleMatch = filter.role ? user.role?.toLowerCase() === filter.role.toLowerCase() : true;
                return genderMatch && ageMatch && roleMatch;
            });
            })
        ).subscribe(users => {
            this.usersSubject.next(users);
        });
    }

    addUser(user:UserModel): void {
        this.http.post<UserModel>('https://dummyjson.com/users/add', user).subscribe(newUser => {
            const current = this.usersSubject.value;
            this.usersSubject.next([newUser, ...current]);
            console.log(current);
        })
    }
}