/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { CursusInstantieService } from './cursusInstantie.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { HttpClientModule } from '@angular/common/http';

describe('Service: CursusInstantie', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CursusInstantieService],
      imports: [HttpClientTestingModule, HttpClientModule]
    });
  });

  it('should ...', inject([CursusInstantieService], (service: CursusInstantieService) => {
    expect(service).toBeTruthy();
  }));
});
