/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { CursusInstantieService } from './cursusInstantie.service';

describe('Service: CursusInstantie', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CursusInstantieService]
    });
  });

  it('should ...', inject([CursusInstantieService], (service: CursusInstantieService) => {
    expect(service).toBeTruthy();
  }));
});
