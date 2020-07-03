/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { CursusWeekoverviewComponent } from './cursus-weekoverview.component';
import { HttpClientModule } from '@angular/common/http';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { RouterModule } from '@angular/router';
import { WeeknumberPipe } from 'src/app/shared/pipes/weeknumber.pipe';
import { FormsModule } from '@angular/forms';

describe('CursusWeekoverviewComponent', () => {
  let component: CursusWeekoverviewComponent;
  let fixture: ComponentFixture<CursusWeekoverviewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CursusWeekoverviewComponent ],
      imports: [
      RouterModule.forRoot([]),
      FormsModule,
      HttpClientTestingModule,
      HttpClientModule,
      MatSnackBarModule,],
      providers: [
        WeeknumberPipe]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CursusWeekoverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
