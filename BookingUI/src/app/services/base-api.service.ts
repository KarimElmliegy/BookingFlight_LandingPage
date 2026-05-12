import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export abstract class BaseApiService<TResponse> {
  constructor( protected http: HttpClient, protected endpoint: string ) {}

  getAll(): Observable<TResponse[]> {
    return this.http.get<TResponse[]>(this.endpoint);
  }

  getById(id: number | string): Observable<TResponse> {
    return this.http.get<TResponse>(`${this.endpoint}/${id}`);
  }

  delete(id: number | string): Observable<void> {
    return this.http.delete<void>(`${this.endpoint}/${id}`);
  }
}

export abstract class CreateApiService<TCreate, TResponse = void> {
  constructor( protected http: HttpClient, protected endpoint: string ) {}

  create(data: TCreate): Observable<TResponse> {
    return this.http.post<TResponse>(this.endpoint, data);
  }
}

export abstract class CrudApiService<TResponse,TCreate,TUpdate> extends BaseApiService<TResponse> {

  create(data: TCreate): Observable<TResponse> {
    return this.http.post<TResponse>(this.endpoint, data);
  }

  update(id: number | string, data: TUpdate): Observable<TResponse> {
    return this.http.put<TResponse>(`${this.endpoint}/${id}`, data);
  }
}
