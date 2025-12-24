import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { UserService } from '../../../services/user.service';
import { UserListModel } from '../../../models/user-list.model';

@Component({
  standalone: true,
  selector: 'app-users-list',
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './users-list.component.html',
})
export class UsersListComponent implements OnInit {
  users: UserListModel[] = [];
  filteredUsers: UserListModel[] = [];
  keyword = '';
  loading = false;
  error = '';

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers() {
    this.loading = true;
    this.error = '';
    this.userService.getUsers().subscribe({
      next: (res) => {
        this.users = res;
        this.filteredUsers = res;
        this.loading = false;
      },
      error: (err) => {
        console.error('Error loading users:', err);
        this.error = 'Failed to load users. Please try again.';
        this.loading = false;
      }
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
