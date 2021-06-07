import { TestBed } from '@angular/core/testing';

import { AproverService } from '../../shared/services/aprover.service';

describe('AproverService', () => {
  let service: AproverService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AproverService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
