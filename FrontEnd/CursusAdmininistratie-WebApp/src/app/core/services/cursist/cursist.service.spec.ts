/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { CursistService } from './cursist.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { HttpClientModule } from '@angular/common/http';

describe('Service: Cursist', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CursistService],
      imports: [HttpClientTestingModule,HttpClientModule ]
    });
  });

  it('should ...', inject([CursistService], (service: CursistService) => {
    expect(service).toBeTruthy();
  }));
});
