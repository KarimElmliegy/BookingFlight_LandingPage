import { Injectable } from '@angular/core';
import { CreateApiService } from './base-api.service';
import { HttpClient } from '@angular/common/http';
import { LoginDto } from '../Interfaces/interfaces/login-dto';
import { environment } from '../../environments/environment';
import { endWith, Observable } from 'rxjs';
import { AuthResponseDto } from '../Interfaces/interfaces/auth-response-dto';
import { RegisterDto } from '../Interfaces/interfaces/register-dto';

@Injectable({
  providedIn: 'root',
})
export class AuthServiceTs {
  private readonly endPoint = '/api/Auth';

  constructor(private http: HttpClient) {}

  Login(data: LoginDto): Observable<AuthResponseDto> {
    return this.http.post<AuthResponseDto>(`${this.endPoint}/login`, data);
  }
  Register(data: RegisterDto): Observable<AuthResponseDto> {
    return this.http.post<AuthResponseDto>(`${this.endPoint}/register`, data);
  }
}
