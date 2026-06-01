import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AlertService {
  private alertSubject = new Subject<any>();

  alertState$ = this.alertSubject.asObservable();

  show(message: string, type: 'success' | 'danger') {
    this.alertSubject.next({
      message,
      type,
    });
  }
}
