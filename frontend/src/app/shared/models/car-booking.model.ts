export enum BookingStatus {
  Booked = 0,
  ReturnRequested = 1,
  ReturnSuccess = 2,
}
export interface CarBooking {
  id: string;
  userId: string;
  carId: string;
  duration: number;
  totalPrice: number;
  startDate: string;
  endDate: string;
  status: BookingStatus;
}
