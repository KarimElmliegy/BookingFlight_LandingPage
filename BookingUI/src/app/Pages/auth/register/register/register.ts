import { Component } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthServiceTs } from '../../../../services/auth.service';

@Component({
  selector: 'app-register',
  imports: [RouterLink, ReactiveFormsModule, CommonModule],
  templateUrl: './register.html',
  styleUrl: './register.css',
  standalone: true,
})
export class Register {
  registerForm: FormGroup;
  showAlert = false;
  alertMessage = '';
  alertType: 'success' | 'danger' = 'success';

  constructor(
    private fb: FormBuilder,
    private authService: AuthServiceTs,
    private router: Router,
  ) {
    this.registerForm = this.fb.group({
      DisplayName: ['', Validators.required],
      UserName: ['', Validators.required],
      Email: ['', [Validators.required, Validators.email]],
      Password: ['', Validators.required],
      ConfirmPassword: ['', Validators.required],
    });
  }

  onSubmit() {
    if (this.registerForm.valid) {
      this.authService.Register(this.registerForm.value).subscribe({
        next: (response: any) => {
          // Store auth data
          localStorage.setItem('token', response.token);
          localStorage.setItem('userName', response.userName);
          localStorage.setItem('displayName', response.displayName);
          localStorage.setItem('email', response.email);
          localStorage.setItem('userId', response.id?.toString() ?? '');

          this.showAlertMessage('Account created successfully! Redirecting...', 'success');
          setTimeout(() => {
            this.router.navigate(['/home']);
          }, 1000);
        },
        error: (error: unknown) => {
          this.showAlertMessage('Registration failed. Please try again.', 'danger');
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
