import { Component } from '@angular/core';
import { Router } from '@angular/router';
import {
  BookingStatus,
  CarBooking,
} from 'src/app/shared/models/car-booking.model';
import { CarBookingCrudService } from 'src/app/shared/services/car-booking-crud.service/car-booking-crud.service';
import { CarCrudService } from 'src/app/shared/services/car-crud.service/car-crud.service';
import { UserAuthService } from 'src/app/shared/services/user-auth.service/user-auth.service';

@Component({
  selector: 'app-car-rentals',
  templateUrl: './car-rentals.component.html',
  styleUrls: ['./car-rentals.component.css'],
})
export class CarRentalsComponent {
  isAdmin: boolean = false;
  BookingStatus = BookingStatus;
  bookings: CarBooking[] = [];
  constructor(
    private carBookingCrudService: CarBookingCrudService,
    private userAuthService: UserAuthService,
    private router: Router,
    private carCrudService: CarCrudService
  ) {}

  ngOnInit(): void {
    this.isAdmin = this.userAuthService.isAdmin();
    if (this.isAdmin) {
      this.getAllCarBookings();
    } else {
      this.getUserCarBookings();
    }

    console.log('MY BOOKINGS: ', this.bookings);
  }

  getAllCarBookings() {
    this.carBookingCrudService.getAllCarBookings().subscribe({
      next: (cars) => {
        this.bookings = cars;
        console.log(cars);
      },
      error: (e) => {
        console.error('Error Fetching Cars:', e);
      },
    });
  }

  getUserCarBookings() {
    let userId = this.userAuthService.getUserId();
    this.carBookingCrudService.getUserCarBookings(userId).subscribe({
      next: (bookings) => {
        this.bookings = bookings;
        console.log('User Booking fetched', bookings);
      },
      error: (e) => {
        console.error('Error fetching bookings: ', e);
      },
    });
  }

  deleteCarBooking(id: string) {
    const check = confirm('Are you sure you want to delete this booking?');
    if (check) {
      this.carBookingCrudService.deleteCarBooking(id).subscribe({
        next: (deletedCarBooking) => {
          console.log('Booking Deleted Successfully: ', deletedCarBooking);
          this.ngOnInit();
        },
      });

      //
    }
  }

  returnCar(id: string) {
    const check = confirm('Are you sure you want to return this car?');
    if (check) {
      let booking;
      this.carBookingCrudService.getCarBookingById(id).subscribe({
        next: (car) => {
          booking = car;
          booking.status = BookingStatus.ReturnRequested;
          this.updateBooking(booking);
          console.log('Requested for return');
        },
      });
    }
  }

  acceptReturnCar(id: string) {
    const check = confirm('Are you sure you want to accept the return?');
    if (check) {
      let booking;
      this.carBookingCrudService.getCarBookingById(id).subscribe({
        next: (car) => {
          booking = car;
          booking.status = BookingStatus.ReturnSuccess;
          this.updateBooking(booking);
          this.updateCarAvailablity(car.carId);
          console.log('Return is successfull');
        },
      });
    }
  }

  updateBooking(booking: CarBooking) {
    this.carBookingCrudService.updateCarBooking(booking).subscribe({
      next: (updatedCar) => {
        console.log('Booking updated: ', updatedCar);
        this.ngOnInit();
      },
    });
  }

  updateCarAvailablity(id: string) {
    let car1;
    this.carCrudService.getCarById(id).subscribe({
      next: (car) => {
        car1 = car;
        car1.availabilityStatus = true;
        this.carCrudService.updateCar(car1).subscribe({
          next: (updatedCar) => {
            console.log('Car availablity updated: ', updatedCar);
          },
        });
      },
    });
  }
}
