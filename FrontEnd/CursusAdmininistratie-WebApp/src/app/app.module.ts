import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { HomeDefaultModule } from './shared/layout/home/home-default/home-default.module';
import { RouterModule } from '@angular/router';
import { homeRoutes } from './core/routing/home-routes';
import { CursusService } from '../app/core/services/cursus/cursus.service';
import { CursusModule } from '../app/modules/cursus/cursus.module';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { AlertService } from '../app/core/services/alert/alert.service';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    HomeDefaultModule,
    CursusModule,
    MatSnackBarModule,
    RouterModule
      .forRoot(homeRoutes)
  ],
  providers: [
    CursusService,
    AlertService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
