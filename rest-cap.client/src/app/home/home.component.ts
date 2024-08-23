import { Component, OnInit } from '@angular/core';
import { HttpsRequestsService } from '../shared-services/https-requests.service';
import { User } from '../common/models.interface';
import { Observable } from 'rxjs';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent implements OnInit {
  users$: Observable<User[]>;
  constructor(private request: HttpsRequestsService) {
    this.users$ = this.request.getUsers();
  }

  ngOnInit(): void {

  }
}
