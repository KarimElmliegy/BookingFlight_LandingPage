import {
  Component,
  OnInit,
  OnDestroy,
  AfterViewInit,
  HostListener,
  ChangeDetectorRef,
  ViewChild,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  ReactiveFormsModule,
  FormsModule,
  FormBuilder,
  FormGroup,
  Validators,
} from '@angular/forms';
import { TripService } from '../../services/trip.service';
import { TicketService } from '../../services/ticket.service';
import { BookTripService } from '../../services/book-trip.service';
import { TripDto } from '../../Interfaces/trip-dto';
import { TicketDto } from '../../Interfaces/ticket-dto';
import { AlertService } from '../../services/alert';

@Component({
  selector: 'app-home',
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './home.html',
  styleUrl: './home.css',
  standalone: true,
})
export class HomePage implements OnInit, AfterViewInit, OnDestroy {
  trips: TripDto[] = [];
  tripQuantities: { [tripId: number]: number } = {};

  carouselIndex = 0;
  visibleCount = 3;

  isLoadingTrips = true;

  constructor(
    private alertService: AlertService,
    private tripService: TripService,
    private bookTripService: BookTripService,
    private cdr: ChangeDetectorRef,
  ) {}

  ngOnInit() {
    this.updateVisibleCount();
    this.loadTrips();
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
        this.trips = tripsArray.map((trip: any) => {
          const id = trip.id ?? trip.Id;
          this.tripQuantities[id] = 1; // Default quantity
          return {
            id,
            fromCity: trip.fromCity ?? trip.FromCity,
            toCity: trip.toCity ?? trip.ToCity,
            price: trip.price ?? trip.Price,
            imageUrl: trip.imageUrl ?? trip.ImageUrl,
          };
        }) as TripDto[];

        this.carouselIndex = 0;
        this.isLoadingTrips = false;
        this.updateVisibleCount();
        this.cdr.detectChanges();
      },
      error: (err) => {
        this.isLoadingTrips = false;
        this.trips = [];
        this.showMessage('Failed to load trips.', 'danger');
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

    const normalized = imageFile.replace(/^\/?images\//i, '');
    return `/images/${normalized}`;
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
  private showMessage(message: string, type: 'success' | 'danger') {
    this.alertService.show(message, type);
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

  bookTrip(tripId: number) {
    const quantity = this.tripQuantities[tripId] || 1;
    this.bookTripService.bookTrip({ TripId: tripId, quantity: quantity }).subscribe({
      next: () => {
        this.showMessage('✈️ Ticket booked successfully!', 'success');
      },
      error: (err: any) => {
        const msg = err?.error?.message || err?.error || 'Booking failed. Please try again.';
        this.showMessage(msg, 'danger');
      },
    });
  }

  scrollTo(sectionId: string) {
    document.getElementById(sectionId)?.scrollIntoView({ behavior: 'smooth' });
  }
}
