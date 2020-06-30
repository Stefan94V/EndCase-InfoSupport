import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CursusComponent } from './cursus/cursus.component';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatMenuModule } from '@angular/material/menu';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';
import { MatTableModule } from '@angular/material/table';
import { MatRadioModule } from '@angular/material/radio';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import {MatDatepickerModule} from '@angular/material/datepicker';
import { MatTabsModule } from '@angular/material/tabs';
import { MatIconModule } from '@angular/material/icon';

import { CursusListComponent } from '../cursus/cursus-list/cursus-list.component';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatInputModule,
    MatCardModule,
    MatFormFieldModule,
    MatButtonModule,
    MatSelectModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatMenuModule,
    MatTableModule,
    MatRadioModule,
    MatSlideToggleModule,
    MatDatepickerModule,
    MatTabsModule,
    MatProgressBarModule,
    MatDividerModule
  ],
  declarations: [
    CursusComponent,
    CursusListComponent]
})
export class CursusModule { }
