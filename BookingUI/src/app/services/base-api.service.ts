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

export abstract class CreateApiService<TCreate, TResponse = void> extends BaseApiService<TResponse>{
  constructor( http: HttpClient, endpoint: string ) {
    super(http,endpoint)
  }

  create(data: TCreate): Observable<TResponse> {
    return this.http.post<TResponse>(this.endpoint, data);
  }
}

export abstract class CrudApiService<TResponse,TCreate,TUpdate> extends  CreateApiService<TCreate, TResponse> {


  constructor(http:HttpClient,endPoint:string) {
    super(http,endPoint);

  }
  update(id: number | string, data: TUpdate): Observable<TResponse> {
    return this.http.put<TResponse>(`${this.endpoint}/${id}`, data);
  }
}
