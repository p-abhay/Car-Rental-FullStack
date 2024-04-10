import { TestBed } from '@angular/core/testing';

import { CarBookingCrudService } from './car-booking-crud.service';

describe('CarBookingCrudService', () => {
  let service: CarBookingCrudService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CarBookingCrudService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
