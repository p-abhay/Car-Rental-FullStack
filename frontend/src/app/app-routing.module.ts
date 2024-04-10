import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { SignupComponent } from './components/signup/signup.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';

import { CarFormComponent } from './components/car-form/car-form.component';
import { CarRentalsComponent } from './components/car-rentals/car-rentals.component';
import { BookingComponent } from './components/booking/booking.component';
import { AdminGuard } from './shared/authguards/admin.guard';
import { AuthGuard } from './shared/authguards/auth.guard';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'signup', component: SignupComponent },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'add-car', component: CarFormComponent, canActivate: [AdminGuard] },
  {
    path: 'dashboard/update-car-form/:id',
    component: CarFormComponent,
    canActivate: [AdminGuard],
  },
  {
    path: 'car-rentals',
    component: CarRentalsComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'dashboard/book/:id', //carId
    component: BookingComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'car-rentals/update-booking/:bookingId',
    component: BookingComponent,
    canActivate: [AdminGuard],
  },
  { path: '**', component: PageNotFoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
