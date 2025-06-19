import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { User } from '../models/User';
import { addUser } from '../ngrx/user.actions';

@Component({
  selector: 'app-add-user',
  imports: [],
  templateUrl: './add-user.html',
  styleUrl: './add-user.css'
})
export class AddUser {
  constructor(private store:Store)
  {}

  handleAddUser(){
    const newUser = new User(103, "John", "Doe", "Admin");
    this.store.dispatch(addUser({user:newUser}));
  }
}
