import { Routes } from '@angular/router';

export const allRoutes: Routes = [
  { path: '', loadComponent: () => import('../app/home/home.component').then(h => h.HomeComponent), pathMatch: 'full' },
  { path: '**', redirectTo: '/', pathMatch: 'full' }
];
