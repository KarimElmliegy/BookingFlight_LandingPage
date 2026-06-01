import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { AlertService } from '../../services/alert';

@Component({
  selector: 'app-alert',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './alert.html',
  styleUrl: './alert.css',
})
export class Alert implements OnInit {
  showAlert = false;
  alertMessage = '';
  alertType: 'success' | 'danger' = 'success';

  constructor(private AlertService: AlertService) {}

  ngOnInit() {
    this.AlertService.alertState$.subscribe((data) => {
      this.alertMessage = data.message;
      this.alertType = data.type;
      this.showAlert = true;

      setTimeout(() => {
        this.showAlert = false;
      }, 4000);
    });
  }
}
