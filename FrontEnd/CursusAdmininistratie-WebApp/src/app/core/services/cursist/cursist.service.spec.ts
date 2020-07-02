/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { CursistService } from './cursist.service';

describe('Service: Cursist', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CursistService]
    });
  });

  it('should ...', inject([CursistService], (service: CursistService) => {
    expect(service).toBeTruthy();
  }));
});
