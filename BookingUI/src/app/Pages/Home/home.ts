import {
  Component,
  OnInit,
  OnDestroy,
  AfterViewInit,
  HostListener,
  ChangeDetectorRef,
} from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TripService } from '../../services/trip.service';
import { TicketService } from '../../services/ticket.service';
import { BookTripService } from '../../services/book-trip.service';
import { ContactUsService } from '../../services/contact-us.service';
import { TripDto } from '../../Interfaces/trip-dto';
import { TicketDto } from '../../Interfaces/ticket-dto';

@Component({
  selector: 'app-home',
  imports: [CommonModule, ReactiveFormsModule, DatePipe],
  templateUrl: './home.html',
  styleUrl: './home.css',
  standalone: true,
})
export class HomePage implements OnInit, AfterViewInit, OnDestroy {
  trips: TripDto[] = [];
  myTickets: TicketDto[] = [];
  contactForm: FormGroup;
  carouselIndex = 0;
  visibleCount = 3;

  showAlert = false;
  alertMessage = '';
  alertType: 'success' | 'danger' = 'success';
  isLoadingTrips = true;

  constructor(
    private tripService: TripService,
    private ticketService: TicketService,
    private bookTripService: BookTripService,
    private contactUsService: ContactUsService,
    private fb: FormBuilder,
    private cdr: ChangeDetectorRef,
  ) {
    this.contactForm = this.fb.group({
      FullName: ['', Validators.required],
      Company: [''],
      Email: ['', [Validators.required, Validators.email]],
      PhoneNumber: [''],
      Address: [''],
      Message: ['', Validators.required],
    });
  }

  ngOnInit() {
    this.updateVisibleCount();
    this.loadTrips();
    this.loadMyTickets();
  }

  ngAfterViewInit() {
    // Force a re-evaluation after the view is fully initialized
    // to ensure visibleCount and layout are correctly applied
    this.updateVisibleCount();
    this.cdr.detectChanges();
  }

  ngOnDestroy() {
    // nothing to tear down — HostListener handles resize
  }

  @HostListener('window:resize')
  onResize() {
    this.updateVisibleCount();
    this.cdr.detectChanges();
  }

  // ✅ Update visibleCount and also re-clamp carouselIndex using new totalSlides
  private updateVisibleCount() {
    const newCount = window.innerWidth < 768 ? 1 : 3;
    if (this.visibleCount !== newCount) {
      this.visibleCount = newCount;
      // Re-clamp index based on new totalSlides
      if (this.carouselIndex >= this.totalSlides) {
        this.carouselIndex = Math.max(0, this.totalSlides - 1);
      }
    }
  }

  loadTrips() {
    this.tripService.getAll().subscribe({
      next: (data: any) => {
        const tripsArray = Array.isArray(data) ? data : (data?.$values ?? []);
        this.trips = tripsArray.map((trip: any) => ({
          id: trip.id ?? trip.Id,
          fromCity: trip.fromCity ?? trip.FromCity,
          toCity: trip.toCity ?? trip.ToCity,
          price: trip.price ?? trip.Price,
          imageUrl: trip.imageUrl ?? trip.ImageUrl,
        })) as TripDto[];

        this.carouselIndex = 0;
        this.isLoadingTrips = false;
        this.updateVisibleCount();
        this.cdr.detectChanges();
      },
      error: (err) => {
        this.isLoadingTrips = false;
        this.trips = [];
        this.displayAlert('Failed to load trips.', 'danger');
        this.cdr.detectChanges();
      },
    });
  }

  getTripImageUrl(imageUrl: string | undefined, fromCity: string, toCity: string): string {
    const imageFile = (imageUrl ?? '').trim();
    if (!imageFile || imageFile.toLowerCase() === 'string') {
      return `https://placehold.co/480x220/e2e8f0/94a3b8?text=${encodeURIComponent(
        fromCity + ' → ' + toCity,
      )}`;
    }

    const normalized = imageFile.replace(/^\/?(assets\/)?(images\/)?/i, '');
    return `assets/images/${normalized}`;
  }

  // ✅ Helper: total number of carousel pages
  get totalSlides(): number {
    if (!this.trips.length) return 0;
    return Math.ceil(this.trips.length / this.visibleCount);
  }
  // ✅ For *ngFor in dots (Angular needs an array)
  get totalSlidesArray(): number[] {
    return Array(this.totalSlides).fill(0);
  }

  // ✅ Override prevSlide / nextSlide / setSlide with correct bounds
  prevSlide() {
    if (this.carouselIndex > 0) {
      this.carouselIndex--;
    }
  }

  nextSlide() {
    if (this.carouselIndex < this.totalSlides - 1) {
      this.carouselIndex++;
    }
  }

  setSlide(index: number) {
    this.carouselIndex = Math.min(Math.max(0, index), this.totalSlides - 1);
  }

  loadMyTickets() {
    this.ticketService.getMyTickets().subscribe({
      next: (data) => {
        this.myTickets = [...data];
        this.cdr.detectChanges();
      },
      error: (error) =>
        this.displayAlert('Failed to load tickets. ' + (error?.error?.message || ''), 'danger'),
    });
  }
  bookTrip(tripId: number) {
    this.bookTripService.bookTrip({ TripId: tripId }).subscribe({
      next: () => {
        this.displayAlert('✈️ Ticket booked successfully!', 'success');
        this.loadMyTickets();
      },
      error: (err) => {
        const msg = err?.error?.message || err?.error || 'Booking failed. Please try again.';
        this.displayAlert(msg, 'danger');
      },
    });
  }

  cancelTicket(ticketId: number) {
    this.ticketService.cancelTicket(ticketId).subscribe({
      next: () => {
        this.displayAlert('Ticket cancelled successfully!', 'success');
        this.loadMyTickets();
      },
      error: () => this.displayAlert('Failed to cancel ticket. Please try again.', 'danger'),
    });
  }

  submitContact() {
    if (this.contactForm.valid) {
      this.contactUsService.sendMessage(this.contactForm.value).subscribe({
        next: () => {
          this.displayAlert('Message sent successfully!', 'success');
          this.contactForm.reset();
        },
        error: () => this.displayAlert('Failed to send message. Please try again.', 'danger'),
      });
    }
  }

  scrollTo(sectionId: string) {
    document.getElementById(sectionId)?.scrollIntoView({ behavior: 'smooth' });
  }

  private displayAlert(message: string, type: 'success' | 'danger') {
    this.alertMessage = message;
    this.alertType = type;
    this.showAlert = true;
    setTimeout(() => (this.showAlert = false), 4000);
  }
}
