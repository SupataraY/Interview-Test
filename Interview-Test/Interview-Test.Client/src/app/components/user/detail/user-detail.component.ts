import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { UserService } from '../../../services/user.service';
import { UserDetailModel } from '../../../models/user-list.model';

@Component({
    standalone: true,
    selector: 'app-user-detail',
    imports: [CommonModule, RouterModule],
    templateUrl: './user-detail.component.html',
})
export class UserDetailComponent implements OnInit {
    user: UserDetailModel | null = null;
    loading = false;
    error = '';

    constructor(
        private route: ActivatedRoute,
        private userService: UserService
    ) {}

    ngOnInit(): void {
        const userId = this.route.snapshot.paramMap.get('id');
        if (userId) {
            this.loadUserDetail(userId);
        }
    }

    loadUserDetail(userId: string) {
        this.loading = true;
        this.error = '';
        this.userService.getUserById(userId).subscribe({
            next: (res) => {
                this.user = res;
                this.loading = false;
            },
            error: (err) => {
                console.error('Error loading user detail:', err);
                this.error = 'Failed to load user details. Please try again.';
                this.loading = false;
            }
        });
    }
}
