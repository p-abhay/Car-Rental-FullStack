import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.prod';
import { Car } from '../../models/car.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CarCrudService {
  private baseApiUrl = environment.baseApiURL;
  constructor(private http: HttpClient) {}

  getAllCars(): Observable<Car[]> {
    return this.http.get<Car[]>(`${this.baseApiUrl}/api/car/all`);
  }

  getAvailableCars(): Observable<Car[]> {
    return this.http.get<Car[]>(`${this.baseApiUrl}/api/car/available`);
  }

  getCarById(id: string): Observable<Car> {
    return this.http.get<Car>(`${this.baseApiUrl}/api/car/${id}`);
  }

  addCar(car: Car): Observable<Car> {
    return this.http.post<Car>(`${this.baseApiUrl}/api/car/add`, car);
  }

  updateCar(updateCar: Car): Observable<Car> {
    return this.http.put<Car>(`${this.baseApiUrl}/api/car/update`, updateCar);
  }

  deleteCar(id: string): Observable<Car> {
    return this.http.delete<Car>(`${this.baseApiUrl}/api/car/delete/${id}`);
  }
}
