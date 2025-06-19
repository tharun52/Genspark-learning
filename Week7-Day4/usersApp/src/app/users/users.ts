import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { UserService } from '../services/UserService';
import { UserModel } from '../models/UserModel';
import { User } from '../user/user';
import { FormsModule } from '@angular/forms';
import { FilterModel } from '../models/FilterModel';
import { fromEvent, debounceTime, map, distinctUntilChanged } from 'rxjs';

@Component({
  selector: 'app-users',
  imports: [User, FormsModule],
  templateUrl: './users.html',
  styleUrl: './users.css'
})
export class Users implements OnInit {
  users:UserModel[] = [];
  filter: FilterModel = new FilterModel();
  
  @ViewChild('searchBox') searchBox!: ElementRef;

  constructor(private userService:UserService)
  {}

  ngOnInit(): void {
    this.userService.users$.subscribe(data => {
      this.users = data;
    });
    this.userService.loadUsers();
  }
  ngAfterViewInit(): void {
    fromEvent<Event>(this.searchBox.nativeElement, 'input')
      .pipe(
        debounceTime(300),
        map((event: Event) => (event.target as HTMLInputElement).value),
        distinctUntilChanged()
      )
      .subscribe(searchText => {
        this.filter.search = searchText.trim();
        this.applyFilter();
      });
  }
  
  applyFilter(): void {
    this.userService.loadUsers(this.filter);
  }
}
