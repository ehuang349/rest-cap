import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { HttpsRequestsService } from '../shared-services/https-requests.service';
import { PaginatedUserResponse, User } from '../common/models.interface';
import { Observable, BehaviorSubject, combineLatest, of } from 'rxjs';
import { map, debounceTime, switchMap, tap } from 'rxjs/operators';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class HomeComponent implements OnInit {
  users$: Observable<User[]> = new Observable<User[]>();
  totalPages$: Observable<number> = of(1); // Initialize with 1
  totalUsers$: Observable<number> = of(0); // Initialize with 0
  private searchQuerySubject = new BehaviorSubject<string>('');
  private pageSubject = new BehaviorSubject<number>(1);
  searchQuery: string = '';
  pageSize: number = 10;
  page: number = 1;
  inputPage: number = 1;

  constructor(private request: HttpsRequestsService, private cdr: ChangeDetectorRef) { }

  ngOnInit(): void {
    // Combine page and search query observables
    combineLatest([
      this.pageSubject.asObservable(),
      this.searchQuerySubject.asObservable()
    ]).pipe(
      switchMap(([page, searchQuery]) => this.loadUsers(page, searchQuery))
    ).subscribe();

    // React to search query changes
    this.searchQuerySubject.pipe(
      debounceTime(300),
      switchMap(query => this.loadUsers(this.pageSubject.value, query))
    ).subscribe();
  }

  loadUsers(page: number, searchQuery: string = this.searchQuery): Observable<User[]> {
    return this.request.getUsers(page, this.pageSize, searchQuery).pipe(
      tap(response => {
        this.totalUsers$ = of(response.totalCount);
        this.totalPages$ = of(Math.ceil(response.totalCount / this.pageSize));
        this.users$ = of(response.users);
        this.cdr.detectChanges();
      }),
      map(response => response.users)
    );
  }

  onSearch(): void {
    console.log('Search Query: ', this.searchQuery);
    this.page = 1; // Reset to first page when searching
    this.pageSubject.next(this.page);
    this.searchQuerySubject.next(this.searchQuery);
  }

  onPageChange(newPage: number): void {
    if (newPage < 1) return;

    this.totalPages$.pipe(
      map(totalPages => {
        if (newPage > totalPages) {
          newPage = totalPages; // Set to last page if newPage exceeds totalPages
        }
        return newPage;
      }),
      tap(page => {
        this.page = page;
        this.pageSubject.next(page);
        this.cdr.detectChanges();
      })
    ).subscribe();
  }

  onPageNumberChange(value: number): void {
    if (value === null || value < 1) return;

    this.totalPages$.pipe(
      map(totalPages => {
        if (value > totalPages) {
          value = totalPages; // Adjust to last page if value exceeds totalPages
        }
        return value;
      }),
      tap(page => {
        this.page = page;
        this.inputPage = page; // Update the page input field
        this.pageSubject.next(page);
        this.cdr.detectChanges();
      })
    ).subscribe();
  }
}
