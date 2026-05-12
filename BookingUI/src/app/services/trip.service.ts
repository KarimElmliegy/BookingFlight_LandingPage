import { Injectable } from '@angular/core';
import { CrudApiService } from './base-api.service';
import { TripDto } from '../Interfaces/interfaces/trip-dto';
import { CreateTripDto } from '../Interfaces/interfaces/create-trip-dto';
import { UpdateTripDto } from '../Interfaces/interfaces/update-trip-dto';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class TripService extends CrudApiService<TripDto, CreateTripDto, UpdateTripDto> {
  constructor(http: HttpClient) {
    super(http, `/api/Trips`);
  }
}
