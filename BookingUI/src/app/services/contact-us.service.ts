import { Injectable } from '@angular/core';
import { CreateContactUsDto } from '../Interfaces/interfaces/create-contact-us-dto';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ContactUsService {
  private readonly endpoint = '/api/ContactUs';

  constructor(private http: HttpClient) {}

  sendMessage(data: CreateContactUsDto): Observable<any> {
    return this.http.post(`${this.endpoint}/send-message`, data);
  }
}
