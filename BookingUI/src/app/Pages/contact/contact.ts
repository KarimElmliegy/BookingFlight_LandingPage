import { Component, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ContactUsService } from '../../services/contact-us.service';
import { Alert } from '../../layouts/alert/alert';
import { AlertService } from '../../services/alert';

@Component({
  selector: 'app-contact',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './contact.html',
  styleUrl: './contact.css',
})
export class Contact {

  contactForm: FormGroup;

  constructor(
    private alertService: AlertService,
    private contactUsService: ContactUsService,
    private fb: FormBuilder,
  ) {
    this.contactForm = this.fb.group({
      FullName: ['', Validators.required],
      Company: [''],
      Email: ['', [Validators.required, Validators.email]],
      PhoneNumber: [''],
      Address: [''],
      Message: ['', Validators.required],
    });
  }

  submitContact() {
    if (this.contactForm.invalid) {
      this.contactForm.markAllAsTouched();
      return;
    }

    this.contactUsService.sendMessage(this.contactForm.value).subscribe({
      next: () => {
        this.alertService.show('Message sent successfully!', 'success');
        this.contactForm.reset();
      },
      error: () =>
        this.alertService.show('Failed to send message. Please try again.', 'danger'),
    });
  }
}
