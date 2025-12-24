import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserListModel } from '../models/user-list.model';

@Injectable({ providedIn: 'root' })
export class UserService {
  constructor(private http: HttpClient) {}

  private baseUrl = 'https://localhost:44375/gateway/api/user';
  getUsers(): Observable<UserListModel[]> {
    return this.http.get<UserListModel[]>(`${this.baseUrl}/GetUsers`);
  }

}
