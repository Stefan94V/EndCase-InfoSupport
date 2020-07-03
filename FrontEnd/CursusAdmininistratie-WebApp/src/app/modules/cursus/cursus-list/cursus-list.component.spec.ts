/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { CursusListComponent } from './cursus-list.component';
import { HttpClientModule } from '@angular/common/http';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { WeeknumberPipe } from 'src/app/shared/pipes/weeknumber.pipe';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

describe('CursusListComponent', () => {
  let component: CursusListComponent;
  let fixture: ComponentFixture<CursusListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CursusListComponent ],
      imports: [
      RouterModule.forRoot([]),
      HttpClientTestingModule,
      HttpClientModule,
      FormsModule,
      MatSnackBarModule,],
      providers: [
        WeeknumberPipe]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CursusListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
