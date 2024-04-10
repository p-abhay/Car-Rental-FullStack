import { identifierName } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';

import { ActivatedRoute, Router } from '@angular/router';
import {
  BookingStatus,
  CarBooking,
} from 'src/app/shared/models/car-booking.model';
import { CarBookingCrudService } from 'src/app/shared/services/car-booking-crud.service/car-booking-crud.service';
import { CarCrudService } from 'src/app/shared/services/car-crud.service/car-crud.service';
import { UserAuthService } from 'src/app/shared/services/user-auth.service/user-auth.service';

@Component({
  selector: 'app-booking',
  templateUrl: './booking.component.html',
  styleUrls: ['./booking.component.css'],
})
export class BookingComponent implements OnInit {
  bookingData: any = {
    fullName: '',
    carMaker: '',
    carModel: '',
    pricePerDay: 0,
    duration: 0,
    totalPrice: 0,
  };
  carBooking: CarBooking = {
    id: '00000000-0000-0000-0000-000000000000',
    userId: '',
    carId: '',
    duration: 0,
    totalPrice: 0,
    startDate: '',
    endDate: '',
    status: BookingStatus.Booked,
  };
  carId: string = '';
  bookingId: string = '';
  userId: string = '';
  editStatus: boolean = false;
  agreeTerms: boolean = false;
  constructor(
    private userAuthService: UserAuthService,
    private carBookingCrudService: CarBookingCrudService,
    private carCrudService: CarCrudService,
    private route: ActivatedRoute,
    private router: Router
  ) {}
  setAgreementStatus() {
    console.log(this.agreeTerms);
    this.agreeTerms = !this.agreeTerms;
  }
  ngOnInit(): void {
    this.carId = this.route.snapshot.paramMap.get('id')!;
    if (this.carId) {
      this.editStatus = false;
      this.carCrudService.getCarById(this.carId).subscribe({
        next: (car) => {
          this.bookingData.carMaker = car.maker;
          this.bookingData.carModel = car.model;
          this.bookingData.pricePerDay = car.rentalPrice;
          this.bookingData.fullName = this.userAuthService.getFullName();
        },
      });
    } else {
      this.editStatus = true;
      this.bookingId = this.route.snapshot.paramMap.get('bookingId')!;
      this.carBookingCrudService.getCarBookingById(this.bookingId).subscribe({
        next: (car) => {
          this.bookingData.duration = car.duration;
          this.bookingData.totalPrice = car.totalPrice;
          this.carId = car.carId;
          this.userId = car.userId;
          console.log('car and user id: ', this.carId, this.userId);
          this.getCarById(this.carId);
          this.getUserById(this.userId);
        },
      });
    }
  }

  getCarById(id: string) {
    this.carCrudService.getCarById(id).subscribe({
      next: (car) => {
        this.bookingData.carMaker = car.maker;
        this.bookingData.carModel = car.model;
        this.bookingData.pricePerDay = car.rentalPrice;
      },
    });
  }

  getUserById(id: string) {
    this.userAuthService.getUserById(id).subscribe({
      next: (user) => {
        this.bookingData.fullName = user.fullName;
      },
    });
  }
  calculateTotalPrice() {
    this.bookingData.totalPrice =
      this.bookingData.pricePerDay * this.bookingData.duration;
  }

  submitBookingForm() {
    if (this.editStatus) {
      this.updateBooking();
      return;
    }
    this.carBooking.userId = this.userAuthService.getUserId();
    this.carBooking.carId = this.carId;

    this.carBooking.totalPrice = this.bookingData.totalPrice;
    // this.bookingData.startDate = this.getCurrentDateTimeInIST();
    // this.bookingData.endDate = this.getCurrentDateTimeInIST(
    //   this.carBooking.duration
    // );
    this.carBooking.duration = this.bookingData.duration;
    console.log('Boking', this.carBooking);
    this.carBookingCrudService.addCarBooking(this.carBooking).subscribe({
      next: (addedCar) => {
        console.log('Booking success: ', addedCar);
        this.router.navigate(['car-rentals']);
      },
    });
  }

  updateBooking() {
    this.carBooking.carId = this.carId;
    this.carBooking.userId = this.userId;
    this.carBooking.duration = this.bookingData.duration;
    this.carBooking.totalPrice = this.bookingData.totalPrice;
    // this.carBooking.startDate = this.getCurrentDateTimeInIST();
    // this.carBooking.endDate = this.getCurrentDateTimeInIST(
    //   this.carBooking.duration
    // );
    this.carBooking.id = this.bookingId;

    this.carBookingCrudService.updateCarBooking(this.carBooking).subscribe({
      next: (updatedCar) => {
        console.log('Car Booking updated successfully:', updatedCar);
        this.router.navigate(['car-rentals']);
      },
    });
  }

  getCurrentDateTimeInIST(durationInDays: number = 0): string {
    // Get the current date and time in UTC
    const currentUtcDate = new Date();

    // Convert UTC time to IST (add 5 hours and 30 minutes)
    const currentIstDate = new Date(
      currentUtcDate.getTime() + 5.5 * 60 * 60 * 1000
    );

    // Calculate the date by adding the duration (in days) to the current date
    const calculatedDate = new Date(
      currentIstDate.getTime() + durationInDays * 24 * 60 * 60 * 1000
    );

    // Format the date and time components in IST
    const day = ('0' + calculatedDate.getDate()).slice(-2);
    const month = ('0' + (calculatedDate.getMonth() + 1)).slice(-2);
    const year = calculatedDate.getFullYear();
    const hours = ('0' + calculatedDate.getHours()).slice(-2);
    const minutes = ('0' + calculatedDate.getMinutes()).slice(-2);
    const seconds = ('0' + calculatedDate.getSeconds()).slice(-2);

    // Combine date and time components into the desired format
    const formattedDateTime = `${day}-${month}-${year} ${hours}:${minutes}:${seconds}`;

    return formattedDateTime; // This will be a string in 'dd-MM-yyyy hh:mm:ss' format
  }
}
