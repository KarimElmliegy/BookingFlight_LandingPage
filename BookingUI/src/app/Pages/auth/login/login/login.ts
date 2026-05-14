import { Component } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthServiceTs } from '../../../../services/auth.service';

@Component({
  selector: 'app-login',
  imports: [RouterLink, ReactiveFormsModule, CommonModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
  standalone: true,
})
export class Login {
  loginForm: FormGroup;
  showAlert = false;
  alertMessage = '';
  alertType: 'success' | 'danger' = 'success';

  constructor(
    private fb: FormBuilder,
    private authService: AuthServiceTs,
    private router: Router,
  ) {
    this.loginForm = this.fb.group({
      Email: ['', [Validators.required, Validators.email]],
      Password: ['', Validators.required],
    });
  }

  onSubmit() {
    if (this.loginForm.valid) {
      this.authService.Login(this.loginForm.value).subscribe({
        next: (response) => {
          localStorage.setItem('token', response.token);
          localStorage.setItem('userName', response.userName);
          localStorage.setItem('displayName', response.displayName);
          localStorage.setItem('email', response.email);
          localStorage.setItem('userId', response.id?.toString() ?? '');
          setTimeout(() => {
            this.router.navigate(['/home']);
          }, 1000);
        },
        error: (error) => {
          this.showAlertMessage('Login failed. Please check your credentials.', 'danger');
        },
      });
    }
  }

  private showAlertMessage(message: string, type: 'success' | 'danger') {
    this.alertMessage = message;
    this.alertType = type;
    this.showAlert = true;
    setTimeout(() => {
      this.showAlert = false;
    }, 3000);
  }
}
