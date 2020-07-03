/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { CursusService } from './cursus.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { HttpClientModule } from '@angular/common/http';

describe('Service: Cursus', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CursusService],
      imports: [HttpClientTestingModule, HttpClientModule]
    });
  });

  it('should ...', inject([CursusService], (service: CursusService) => {
    expect(service).toBeTruthy();
  }));
});
