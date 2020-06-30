import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeDefaultComponent } from './home-default.component';
import { HomeHeaderComponent } from '../home-header/home-header.component';
import { HomeSidebarComponent } from '../home-sidebar/home-sidebar.component';
import { RouterModule } from '@angular/router';

import { MatSidenavModule } from '@angular/material/sidenav';
import { MatDividerModule } from '@angular/material/divider';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatMenuModule } from '@angular/material/menu';
import { MatListModule } from '@angular/material/list';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';

import { FlexLayoutModule } from '@angular/flex-layout';
import { MatCardModule } from '@angular/material/card';
import { ReactiveFormsModule } from '@angular/forms';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { from } from 'rxjs';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    MatSidenavModule,
    MatDividerModule,
    MatToolbarModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatButtonModule,
    MatMenuModule,
    MatListModule,
    MatInputModule,
    MatCardModule,
    MatFormFieldModule,
    MatSelectModule,
    ReactiveFormsModule,
    FlexLayoutModule,
  ],
  declarations: [
    HomeDefaultComponent,
    HomeHeaderComponent,
    HomeSidebarComponent
  ]
})
export class HomeDefaultModule { }
