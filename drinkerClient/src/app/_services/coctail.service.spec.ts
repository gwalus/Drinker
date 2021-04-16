import { TestBed } from '@angular/core/testing';

import { CoctailService } from './coctail.service';

describe('CoctailService', () => {
  let service: CoctailService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CoctailService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
