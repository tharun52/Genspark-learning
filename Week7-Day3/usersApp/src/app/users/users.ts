import { Component, OnInit } from '@angular/core';
import { UserService } from '../services/UserService';
import { UserModel } from '../models/UserModel';
import { User } from '../user/user';
import { FormsModule } from '@angular/forms';
import { FilterModel } from '../models/FilterModel';

@Component({
  selector: 'app-users',
  imports: [User, FormsModule],
  templateUrl: './users.html',
  styleUrl: './users.css'
})
export class Users implements OnInit {
  users:UserModel[] = [];
  filter: FilterModel = new FilterModel();
  
  constructor(private userService:UserService)
  {}

  ngOnInit(): void {
    this.userService.users$.subscribe(data => {
      this.users = data;
    });
    this.userService.loadUsers();
  }
  applyFilter(): void {
    this.userService.loadUsers(this.filter);
  }
}
