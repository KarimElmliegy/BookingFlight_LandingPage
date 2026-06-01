import { CommonModule, DatePipe } from '@angular/common';
import { AfterViewInit, ChangeDetectorRef, Component, HostListener, OnInit } from '@angular/core';
import { TicketService } from './../../services/ticket.service';
import { TicketDto } from '../../Interfaces/ticket-dto';
import { AlertService } from '../../services/alert';

@Component({
  selector: 'app-my-tickets',
  standalone: true,
  imports: [CommonModule, DatePipe],
  templateUrl: './my-tickets.html',
  styleUrl: './my-tickets.css',
})
export class MyTickets implements OnInit, AfterViewInit {
  myTickets: TicketDto[] = [];

  visibleCount = 4;
  carouselIndex = 0;
  isLoading = true;

  constructor(
    private alertService: AlertService,
    private ticketService: TicketService,
    private cdr: ChangeDetectorRef,
  ) {}

  ngOnInit(): void {
    this.updateVisibleCount();
    this.loadMyTickets();
  }

  ngAfterViewInit(): void {
    this.updateVisibleCount();
    this.cdr.detectChanges();
  }

  @HostListener('window:resize')
  onResize() {
    this.updateVisibleCount();
    this.cdr.detectChanges();
  }

  private updateVisibleCount() {
    const newCount = window.innerWidth < 768 ? 1 : 4;

    if (this.visibleCount !== newCount) {
      this.visibleCount = newCount;

      if (this.carouselIndex >= this.totalSlides) {
        this.carouselIndex = Math.max(0, this.totalSlides - 1);
      }
    }
  }

  loadMyTickets() {
    this.isLoading = true;
    this.ticketService.getMyTickets().subscribe({
      next: (data: any) => {
        const ticketsArray = Array.isArray(data) ? data : (data?.$values ?? []);

        this.myTickets = ticketsArray.map((ticket: any) => ({
          id: ticket.id ?? ticket.Id,
          fromCity: ticket.fromCity ?? ticket.FromCity,
          toCity: ticket.toCity ?? ticket.ToCity,
          bookingDate: ticket.bookingDate ?? ticket.BookingDate,
          status: ticket.status ?? ticket.Status,
          ImageUrl: ticket.imageUrl ?? ticket.ImageUrl,
          Quantity: ticket.quantity ?? ticket.Quantity,
        })) as TicketDto[];

        this.carouselIndex = 0;
        this.updateVisibleCount();
        this.isLoading = false;
        this.cdr.detectChanges();
      },
      error: (error) => {
        this.isLoading = false;
        this.alertService.show(
          'Failed to load tickets. ' + (error?.error?.message || ''),
          'danger',
        );
        this.cdr.detectChanges();
      },
    });
  }

  cancelTicket(ticketId: number) {
    this.ticketService.cancelTicket(ticketId).subscribe({
      next: () => {
        this.alertService.show('Ticket cancelled successfully!', 'success');
        this.loadMyTickets();
      },
      error: () => this.alertService.show('Failed to cancel ticket. Please try again.', 'danger'),
    });
  }

  get totalSlides(): number {
    if (!this.myTickets.length) return 0;
    return Math.ceil(this.myTickets.length / this.visibleCount);
  }

  get totalSlidesArray(): number[] {
    return Array(this.totalSlides).fill(0);
  }

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

  getTicketImageUrl(imageUrl: string | undefined, fromCity: string, toCity: string): string {
    const imageFile = (imageUrl ?? '').trim();

    if (!imageFile) {
      return `https://placehold.co/480x260/e2e8f0/94a3b8?text=${encodeURIComponent(
        fromCity + ' → ' + toCity,
      )}`;
    }

    return `assets/images/${imageFile}`;
  }
}
