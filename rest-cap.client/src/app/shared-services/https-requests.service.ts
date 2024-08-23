import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { User } from '../common/models.interface';
import { env } from '../../env/env';
import { API_CHILD_ROUTES } from '../common/constants';

@Injectable({
  providedIn: 'root'
})
export class HttpsRequestsService {
  constructor(private http: HttpClient) { }


  getUsers(): Observable<User[]> {
    const apiUrl = `${env.apiBaseUrl}${API_CHILD_ROUTES.GET_USERS}`;
    const headers = new HttpHeaders({
      'api-key': env.apiKey
    });
    return this.http.get<User[]>(apiUrl, { headers }).pipe(
      tap({
        next: (users) => console.log('Fetched users:', users),
        error: (error) => console.error('ferror fetching users', error)
      })
    );
  }
}
