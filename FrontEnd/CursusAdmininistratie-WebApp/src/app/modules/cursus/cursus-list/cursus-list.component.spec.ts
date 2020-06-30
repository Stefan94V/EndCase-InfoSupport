/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { CursusListComponent } from './cursus-list.component';

describe('CursusListComponent', () => {
  let component: CursusListComponent;
  let fixture: ComponentFixture<CursusListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CursusListComponent ]
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
