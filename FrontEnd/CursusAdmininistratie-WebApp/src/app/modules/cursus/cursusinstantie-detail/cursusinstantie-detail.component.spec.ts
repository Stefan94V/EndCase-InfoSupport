/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { CursusinstantieDetailComponent } from './cursusinstantie-detail.component';
import { HttpClientModule } from '@angular/common/http';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { WeeknumberPipe } from 'src/app/shared/pipes/weeknumber.pipe';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';

describe('CursusinstantieDetailComponent', () => {
  let component: CursusinstantieDetailComponent;
  let fixture: ComponentFixture<CursusinstantieDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CursusinstantieDetailComponent ],
      imports: [
      RouterModule.forRoot([]),
      BrowserAnimationsModule,
      HttpClientTestingModule,
      HttpClientModule,
      FormsModule,
      MatSnackBarModule],
      providers: [
        WeeknumberPipe]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CursusinstantieDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

});
