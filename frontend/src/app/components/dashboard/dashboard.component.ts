import { Component, OnInit } from '@angular/core';
import { Car } from 'src/app/shared/models/car.model';
import { CarCrudService } from 'src/app/shared/services/car-crud.service/car-crud.service';
import { UserAuthService } from 'src/app/shared/services/user-auth.service/user-auth.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
})
export class DashboardComponent implements OnInit {
  cars: Car[] = [];
  isAdmin: boolean = false;

  //loading screen
  loading: boolean = true;

  //Search options and filtered cars

  searchParams = {
    maker: '',
    model: '',
    rentalPrice: null,
  };

  filteredCars: Car[] = [];

  constructor(
    private carCrudService: CarCrudService,
    public userAuthService: UserAuthService
  ) {}

  ngOnInit(): void {
    this.isAdmin = this.userAuthService.isAdmin();
    console.log('INSIDE DASH NGONIT');
    console.log('from service: ', this.userAuthService.isAdmin());
    console.log('local dash: ', this.isAdmin);
    if (this.isAdmin) {
      this.getAllCars();
    } else {
      this.getAvailableCars();
    }
  }

  getAllCars() {
    this.carCrudService.getAllCars().subscribe({
      next: (cars) => {
        this.cars = cars;
        this.filteredCars = cars;
        console.log(cars);
        this.loading = false;
      },
      error: (e) => {
        console.error('Error Fetching Cars:', e);
      },
    });
  }
  getAvailableCars() {
    this.carCrudService.getAvailableCars().subscribe({
      next: (cars) => {
        this.cars = cars;
        this.filteredCars = cars;
        console.log(cars);
        this.loading = false;
      },
      error: (e) => {
        console.error('Error Fetching Cars:', e);
      },
    });
  }

  deleteCar(id: string) {
    const check = confirm('Are you sure you want to delete this?');
    if (check) {
      this.carCrudService.deleteCar(id).subscribe({
        next: (car) => {
          console.log('Deleted Successfully: ', car);
          this.getAllCars();
        },
      });
    }
  }
  filterCars() {
    this.filteredCars = this.cars.filter((car) => {
      const makerMatch =
        !this.searchParams.maker ||
        car.maker.toLowerCase().includes(this.searchParams.maker.toLowerCase());
      const modelMatch =
        !this.searchParams.model ||
        car.model.toLowerCase().includes(this.searchParams.model.toLowerCase());
      const priceMatch =
        !this.searchParams.rentalPrice ||
        car.rentalPrice <= this.searchParams.rentalPrice;
      return makerMatch && modelMatch && priceMatch;
    });

    // If all search fields are empty, reset the filteredCars to show all cars.
    if (
      !this.searchParams.maker &&
      !this.searchParams.model &&
      !this.searchParams.rentalPrice
    ) {
      this.filteredCars = [...this.cars];
    }
  }
}
