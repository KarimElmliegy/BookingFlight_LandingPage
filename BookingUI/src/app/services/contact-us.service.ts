import { Injectable } from '@angular/core';
import { CreateContactUsDto } from '../Interfaces/create-contact-us-dto';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CreateApiService } from './base-api.service';

@Injectable({
  providedIn: 'root',
})
export class ContactUsService extends CreateApiService<CreateContactUsDto> {
  // private readonly endpoint = '/api/ContactUs';

  constructor(http: HttpClient) {
    super(http, '/api/ContactUs/send-message');
  }

  sendMessage(data: CreateContactUsDto) {
    return this.create(data);
  }
}
