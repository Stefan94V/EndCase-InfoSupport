/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { CursusService } from './cursus.service';

describe('Service: Cursus', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CursusService]
    });
  });

  it('should ...', inject([CursusService], (service: CursusService) => {
    expect(service).toBeTruthy();
  }));
});
