/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { AlertService } from './alert.service';
import { MatSnackBarModule } from '@angular/material/snack-bar';

describe('Service: Alert', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AlertService],
      imports: [MatSnackBarModule]
    });
  });

  it('should ...', inject([AlertService], (service: AlertService) => {
    expect(service).toBeTruthy();
  }));
});
