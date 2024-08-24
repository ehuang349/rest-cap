import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { HttpsRequestsService } from '../shared-services/https-requests.service';
import { User } from '../common/models.interface';
import { Observable, BehaviorSubject } from 'rxjs';
import { map, debounceTime, switchMap } from 'rxjs/operators';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';


@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class HomeComponent implements OnInit {
  users$: Observable<User[]>;
  filteredUsers$: Observable<User[]> = new Observable<User[]>();
  private searchQuerySubject = new BehaviorSubject<string>('');
  searchQuery: string = '';
  constructor(private request: HttpsRequestsService) {
    this.users$ = this.request.getUsers();
    
  }
  
  ngOnInit(): void {
    this.filteredUsers$ = this.searchQuerySubject.pipe(debounceTime(300), switchMap(query => this.filterUsers(query)));
    this.onSearch();
  }

  onSearch(): void {
    console.log('Search Query: ', this.searchQuery);
    this.searchQuerySubject.next(this.searchQuery);
  }

  private filterUsers(query: string): Observable<User[]> {
    console.log('filterUsers: ', query);
    if (!query.trim()) return this.users$;

    return this.users$.pipe(
      map(users => {
        const filtered = users.filter(user =>
          user.userName.toLowerCase().includes(query.toLowerCase()) ||
          user.email.toLowerCase().includes(query.toLowerCase())
        );
        console.log('Filtered Users:', filtered); // Debugging line
        return filtered;
      })
    );
  }
}
