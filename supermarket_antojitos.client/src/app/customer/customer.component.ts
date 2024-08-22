import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CustomerService } from '../services/customer/customer.service';
import { Customer } from '../models/customer.model';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrl: './customer.component.css'
})
export class CustomerComponent implements OnInit {
  customers: Customer[] = [];
  customerForm: FormGroup;

  constructor(private customerService: CustomerService, private fb: FormBuilder, private snackBar: MatSnackBar) {
    this.customerForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      dni: ['', [Validators.required, Validators.pattern(/^\d{1,10}$/)]],
      phoneNumber: ['', [Validators.required, Validators.pattern(/^\d{1,10}$/)]],
      email: ['', [Validators.required, Validators.email]],
      address: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.customerService.getCustomers().subscribe((data: Customer[]) => {
      this.customers = data;
    });
  }

  onSubmit(): void {
    if (this.customerForm.valid) {
      this.customerService.addCustomer(this.customerForm.value).subscribe(
        response => {
          this.snackBar.open('Customer added successfully!', 'Close', {
            duration: 3000,
            panelClass: ['snackbar-success']
          });
          // Reset the form after success
          this.customerForm.reset();
        },
        error => {
          this.snackBar.open('An unexpected error occurred. Please try again.', 'Close', {
            duration: 3000,
            panelClass: ['snackbar-error']
          });
        }
      );
    }
  }
}
