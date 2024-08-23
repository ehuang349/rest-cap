import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrl: './app.component.scss',
    standalone: true,
    imports: [RouterOutlet]
})
export class AppComponent implements OnInit {

  constructor() {

  }

  ngOnInit(): void {
    
  }

  title = 'rest-cap.client';
}
