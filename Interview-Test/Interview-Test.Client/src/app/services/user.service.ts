import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserListModel, UserDetailModel } from '../models/user-list.model';

@Injectable({ providedIn: 'root' })
export class UserService {
  constructor(private http: HttpClient) {}

  private baseUrl = 'http://localhost:44375/gateway/api/user';
  
  private getHeaders(): HttpHeaders {
    return new HttpHeaders({
      'x-api-key': 'interview-test-2024'
    });
  }
  
  getUsers(): Observable<UserListModel[]> {
    return this.http.get<UserListModel[]>(`${this.baseUrl}/GetUserAll`, {
      headers: this.getHeaders()
    });
  }

  getUserById(id: string): Observable<UserDetailModel> {
    return this.http.get<UserDetailModel>(`${this.baseUrl}/GetUserById/${id}`, {
      headers: this.getHeaders()
    });
  }
}
