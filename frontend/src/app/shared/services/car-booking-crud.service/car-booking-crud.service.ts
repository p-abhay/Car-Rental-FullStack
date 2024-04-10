import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.prod';
import { Car } from '../../models/car.model';
import { CarBooking } from '../../models/car-booking.model';

@Injectable({
  providedIn: 'root',
})
export class CarBookingCrudService {
  private baseApiUrl = environment.baseApiURL;
  constructor(private http: HttpClient) {}

  getAllCarBookings(): Observable<CarBooking[]> {
    return this.http.get<CarBooking[]>(
      `${this.baseApiUrl}/api/car-bookings/all`
    );
  }

  getUserCarBookings(userId: string): Observable<CarBooking[]> {
    return this.http.get<CarBooking[]>(
      `${this.baseApiUrl}/api/car-bookings/user/${userId}`
    );
  }

  getCarBookingById(id: string): Observable<CarBooking> {
    return this.http.get<CarBooking>(
      `${this.baseApiUrl}/api/car-bookings/${id}`
    );
  }

  addCarBooking(car: CarBooking): Observable<CarBooking> {
    return this.http.post<CarBooking>(
      `${this.baseApiUrl}/api/car-booking/add`,
      car
    );
  }

  updateCarBooking(updateCar: CarBooking): Observable<CarBooking> {
    return this.http.put<CarBooking>(
      `${this.baseApiUrl}/api/car-booking/update`,
      updateCar
    );
  }

  deleteCarBooking(id: string): Observable<CarBooking> {
    return this.http.delete<CarBooking>(
      `${this.baseApiUrl}/api/car-bookings/delete/${id}`
    );
  }
}
