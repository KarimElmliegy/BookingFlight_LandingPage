import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BookTripDto } from '../Interfaces/interfaces/book-trip-dto';

@Injectable({
  providedIn: 'root',
})
export class BookTripService {
  private readonly endpoint = '/api/Tickets';

  constructor(private http: HttpClient) {}

  bookTrip(data: BookTripDto): Observable<any> {
    return this.http.post(`${this.endpoint}/book`, data);
  }
}
