import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BookTripDto } from '../Interfaces/book-trip-dto';
import { CreateApiService } from './base-api.service';
import { TicketDto } from '../Interfaces/ticket-dto';

@Injectable({
  providedIn: 'root',
})
export class BookTripService extends CreateApiService<BookTripDto> {
  constructor(http: HttpClient) {
    super(http, '/api/Tickets/book');
  }

  bookTrip(data: BookTripDto) {
    return this.create(data);
  }
}
