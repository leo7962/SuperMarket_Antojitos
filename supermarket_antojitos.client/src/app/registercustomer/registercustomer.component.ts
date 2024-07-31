import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-registercustomer',
  templateUrl: './registercustomer.component.html',
  styleUrl: './registercustomer.component.css'
})
export class RegistercustomerComponent {
  @Output() customerRegistered = new EventEmitter<void>();

  registerCustomer() {
    
    this.customerRegistered.emit();
  }
}
