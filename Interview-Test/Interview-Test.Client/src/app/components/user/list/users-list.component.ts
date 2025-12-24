import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { UserService } from '../../../services/user.service';
import { UserListModel } from '../../../models/user-list.model';

@Component({
  standalone: true,
  selector: 'app-users-list',
  imports: [CommonModule, FormsModule],
  templateUrl: './users-list.component.html',
})
export class UsersListComponent implements OnInit {
  users: UserListModel[] = [];
  filteredUsers: UserListModel[] = [];
  keyword = '';

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers() {
    this.userService.getUsers().subscribe(res => {
      this.users = res;
      this.filteredUsers = res;
    });
  }

  search() {
    const key = this.keyword.toLowerCase();
    this.filteredUsers = this.users.filter(u =>
      u.id.toLowerCase().includes(key) ||
      u.userId.toLowerCase().includes(key) ||
      u.username.toLowerCase().includes(key) ||
      u.firstName.toLowerCase().includes(key) ||
      u.lastName.toLowerCase().includes(key)
    );
  }
}
