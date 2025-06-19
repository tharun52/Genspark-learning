import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";
import { UserModel } from "../models/UserModel";
import { FilterModel } from "../models/FilterModel";

@Injectable()
export class UserService {
  private usersSubject = new BehaviorSubject<UserModel[]>([]);
  users$ = this.usersSubject.asObservable();

  private allUsers: UserModel[] = [];
  private nextId = 1;

  constructor() {}

  addUser(user: UserModel): void {
    const newUser = { ...user, id: this.nextId++ };
    this.allUsers.unshift(newUser);
    this.usersSubject.next([...this.allUsers]);
  }

  loadUsers(filter?: FilterModel): void {
  let filtered = [...this.allUsers];

  if (filter) {
    if (filter.gender) {
      filtered = filtered.filter(u => u.gender.toLowerCase() === filter.gender.toLowerCase());
    }

    if (filter.role) {
      filtered = filtered.filter(u => u.role.toLowerCase() === filter.role.toLowerCase());
    }

    if (filter.ageRange) {
      const [min, max] = filter.ageRange.includes('+')
        ? [parseInt(filter.ageRange), Infinity]
        : filter.ageRange.split('-').map(Number);
      filtered = filtered.filter(u => u.age >= min && u.age <= max);
    }

    if (filter.search) {
      const term = filter.search.toLowerCase();
      filtered = filtered.filter(u =>
        u.firstName.toLowerCase().includes(term) ||
        u.lastName.toLowerCase().includes(term) ||
        u.username.toLowerCase().includes(term) ||
        u.email.toLowerCase().includes(term)
      );
    }
  }

    this.usersSubject.next(filtered);
  }
}
