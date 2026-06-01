import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

interface BookingSummary {
  from: string;
  fromCode: string;
  to: string;
  toCode: string;
  departureTime: string;
  arrivalTime: string;
  duration: string;
  date: string;
  passengers: number;
  passengerType: string;
  class: string;
  baseFare: number;
  taxes: number;
  serviceFee: number;
  currency: string;
}

interface PassengerInfo {
  fullName: string;
  email: string;
  phoneNumber: string;
  countryCode: string;
  nationality: string;
  address: string;
  city: string;
  postalCode: string;
  country: string;
  specialRequests: string;
}

@Component({
  selector: 'app-checkout',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './checkout.html',
  styleUrls: ['./checkout.css'],
})
export class Checkout implements OnInit {
  currentStep: number = 1;
  steps = [
    { number: 1, title: 'Passenger Info', icon: 'fa-user', subtitle: 'Your details' },
    { number: 2, title: 'Seat & Extras', icon: 'fa-chair', subtitle: 'Choose options' },
    { number: 3, title: 'Payment', icon: 'fa-credit-card', subtitle: 'Secure checkout' },
    { number: 4, title: 'Confirmation', icon: 'fa-check', subtitle: 'Your ticket' },
  ];

  bookingSummary: BookingSummary = {
    from: 'Alexandria',
    fromCode: 'ALY',
    to: 'Istanbul',
    toCode: 'IST',
    departureTime: '08:30 AM',
    arrivalTime: '11:15 AM',
    duration: '2h 45m',
    date: '20 May 2025',
    passengers: 1,
    passengerType: 'Adult',
    class: 'Economy',
    baseFare: 7000,
    taxes: 1200,
    serviceFee: 200,
    currency: 'EGP',
  };

  passengerInfo: PassengerInfo = {
    fullName: 'Karim Ayman',
    email: 'karim.ayman@example.com',
    phoneNumber: '010 1234 5678',
    countryCode: '+20',
    nationality: 'Egypt',
    address: '123 Tahrir Street, Cairo, Egypt',
    city: 'Cairo',
    postalCode: '11511',
    country: 'Egypt',
    specialRequests: '',
  };

  countries = ['Egypt', 'United States', 'United Kingdom', 'Germany', 'France'];
  countryCodes = [
    { country: 'Egypt', code: '+20' },
    { country: 'United States', code: '+1' },
    { country: 'United Kingdom', code: '+44' },
    { country: 'Germany', code: '+49' },
    { country: 'France', code: '+33' },
  ];

  ngOnInit() {
    // Initialize with default data
  }

  get totalAmount(): number {
    return (
      this.bookingSummary.baseFare + this.bookingSummary.taxes + this.bookingSummary.serviceFee
    );
  }

  onEditBooking() {
    console.log('Edit booking clicked');
  }

  onProceedToPayment() {
    if (this.validatePassengerInfo()) {
      this.currentStep = 3;
      console.log('Proceeding to payment');
    }
  }

  validatePassengerInfo(): boolean {
    if (
      !this.passengerInfo.fullName ||
      !this.passengerInfo.email ||
      !this.passengerInfo.phoneNumber
    ) {
      alert('Please fill in all required fields');
      return false;
    }
    return true;
  }

  onCountryChange(country: string) {
    const selected = this.countryCodes.find((c) => c.country === country);
    if (selected) {
      this.passengerInfo.countryCode = selected.code;
    }
  }

  isStepActive(stepNumber: number): boolean {
    return this.currentStep === stepNumber;
  }

  isStepCompleted(stepNumber: number): boolean {
    return stepNumber < this.currentStep;
  }

  goToStep(stepNumber: number) {
    if (stepNumber <= this.currentStep) {
      this.currentStep = stepNumber;
    }
  }
}
