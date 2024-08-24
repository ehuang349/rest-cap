import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { PaginatedUserResponse } from '../common/models.interface';
import { env } from '../../env/env';
import { API_CHILD_ROUTES } from '../common/constants';

@Injectable({
  providedIn: 'root'
})
export class HttpsRequestsService {
  constructor(private http: HttpClient) { }


  getUsers(page: number = 1, pageSize: number = 10, userName?: string, email?: string): Observable<PaginatedUserResponse> {
    const apiUrl = `${env.apiBaseUrl}${API_CHILD_ROUTES.GET_USERS}`;
    const headers = new HttpHeaders({
      'api-key': env.apiKey
    });

    let params = new HttpParams().set('page', page.toString()).set('pageSize', pageSize.toString());

    if (userName) {
      params = params.set('userName', userName);
    }

    if (email) {
      params = params.set('email', email);
    }

    return this.http.get<PaginatedUserResponse>(apiUrl, { headers, params }).pipe(
      tap({
        next: (response) => {
          console.log('Fetched users:', response.users);
          console.log('Pagination info:', response.totalCount, response.page, response.pageSize)
        },
        error: (error) => {
          console.error('Error fetching users', error);
        }
      })
    );

  }
}
