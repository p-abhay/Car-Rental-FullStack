import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Car } from 'src/app/shared/models/car.model';
import { CarCrudService } from 'src/app/shared/services/car-crud.service/car-crud.service';

@Component({
  selector: 'app-car-form',
  templateUrl: './car-form.component.html',
  styleUrls: ['./car-form.component.css'],
})
export class CarFormComponent implements OnInit {
  car: Car = {
    id: '00000000-0000-0000-0000-000000000000',
    maker: '',
    model: '',
    availabilityStatus: true,
    rentalPrice: 0,
    image: '',
  };
  carId: string = '';
  isEditing: boolean = false;
  constructor(
    private route: ActivatedRoute,
    private carCrudService: CarCrudService,
    private router: Router
  ) {}

  ngOnInit(): void {
    console.log('INSIDE CAR FORM ______');
    this.carId = this.route.snapshot.paramMap.get('id')!;
    if (this.carId) {
      this.carCrudService.getCarById(this.carId).subscribe({
        next: (car) => {
          this.car = car;
          this.isEditing = true;
        },
        error: (e) => {
          console.error('Error fetching car: ', e);
        },
      });
    } else {
      this.isEditing = false;
    }
  }

  onSubmit() {
    if (this.isEditing) {
      this.updateCar();
    } else {
      this.addCar();
    }
    this.router.navigate(['dashboard']);
  }
  addCar() {
    this.carCrudService.addCar(this.car).subscribe({
      next: (addedCar) => {
        console.log('Car added Successfully: ', addedCar);
      },
      error: (e) => {
        console.error('Error adding car: ', e);
      },
    });
  }
  updateCar() {
    this.carCrudService.updateCar(this.car).subscribe({
      next: (updatedCar) => {
        console.log('Car Updated Successfully: ', updatedCar);
      },
      error: (e) => {
        console.error('Error updating car: ', e);
      },
    });
  }
}
