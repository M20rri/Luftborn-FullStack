import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserDto } from './user.dto';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private apiUrl = 'https://localhost:7113/api/Users'; // Update with your API URL

  constructor(private http: HttpClient) {}

  getUsers(): Observable<UserDto[]> {
    return this.http.get<UserDto[]>(`${this.apiUrl}`);
  }

  getUser(id: number): Observable<UserDto> {
    return this.http.get<UserDto>(`${this.apiUrl}/${id}`);
  }

  addUser(user: any): Observable<UserDto> {
    return this.http.post<UserDto>(`${this.apiUrl}`, user);
  }

  updateUser(user: any): Observable<UserDto> {
    return this.http.put<UserDto>(`${this.apiUrl}`, user);
  }

  deleteUser(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
