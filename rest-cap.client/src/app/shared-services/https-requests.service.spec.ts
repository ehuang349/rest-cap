import { TestBed } from '@angular/core/testing';

import { HttpsRequestsService } from './https-requests.service';

describe('HttpsRequestsService', () => {
  let service: HttpsRequestsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(HttpsRequestsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
