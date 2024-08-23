import { BrowserModule, bootstrapApplication } from '@angular/platform-browser';
import { withInterceptorsFromDi, provideHttpClient } from '@angular/common/http';
import { allRoutes } from './app/app-routing.module';
import { AppComponent } from './app/app.component';
import { importProvidersFrom } from '@angular/core';
import { PreloadAllModules, provideRouter, withPreloading } from '@angular/router';

bootstrapApplication(AppComponent, {
  providers: [
    provideRouter(allRoutes, withPreloading(PreloadAllModules)),
    importProvidersFrom(BrowserModule),
    provideHttpClient(withInterceptorsFromDi())
  ]
}).catch(err => console.error(err));
