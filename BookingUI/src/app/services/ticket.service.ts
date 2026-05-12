import { Injectable } from '@angular/core';
import { TicketDto } from '../Interfaces/interfaces/ticket-dto';
import { HttpClient } from '@angular/common/http';
import { BaseApiService } from './base-api.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class TicketService extends BaseApiService<TicketDto> {
  constructor(http: HttpClient) {
    super(http, `/api/Tickets`);
  }

  getMyTickets(): Observable<TicketDto[]> {
    return this.http.get<TicketDto[]>(`${this.endpoint}/my-tickets`);
  }

  cancelTicket(id: number): Observable<void> {
    return this.http.delete<void>(`${this.endpoint}/${id}`);
  }
}
