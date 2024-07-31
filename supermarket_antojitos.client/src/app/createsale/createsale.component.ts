import { Component, OnInit } from '@angular/core';
import { Product } from '../models/product.model';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CustomerService } from '../services/customer/customer.service';
import { SaleService } from '../services/sale/sale.service';
import { ProductService } from '../services/product/product.service';
import { Sale } from '../models/sale.model';

@Component({
  selector: 'app-createsale',
  templateUrl: './createsale.component.html',
  styleUrl: './createsale.component.css'
})
export class CreatesaleComponent implements OnInit {
  saleForm: FormGroup;
  customerExists: boolean = false;
  isRegisteringCustomer: boolean = false;
  products: Product[] = [];
  currentDate: Date = new Date();
  productPriceCache: { [key: number]: number } = {};

  constructor(
    private fb: FormBuilder,
    private saleService: SaleService,
    private customerService: CustomerService,
    private productService: ProductService
  ) {
    this.saleForm = this.fb.group({
      dni: ['', [Validators.required, Validators.pattern(/^\d{1,10}$/)]],
      saleDetails: this.fb.array([])
    });
  }

  ngOnInit(): void {
    this.productService.getProducts().subscribe(products => this.products = products);

    // Suscribirse a los cambios en el DNI para manejar habilitación de botones
    this.saleForm.get('dni')?.valueChanges.subscribe(() => this.onDniChange());
  }

  get saleDetails(): FormArray {
    return this.saleForm.get('saleDetails') as FormArray;
  }

  addSaleDetail(): void {
    this.saleDetails.push(this.fb.group({
      productId: ['', Validators.required],
      quantity: [1, [Validators.required, Validators.min(1)]],
      unitPrice: [{ value: '', disabled: true }, Validators.required],
      totalPrice: [{ value: '', disabled: true }, Validators.required]
    }));
  }

  removeSaleDetail(index: number): void {
    this.saleDetails.removeAt(index);
  }

  calculateTotalPrice(detail: FormGroup): void {
    const productId = detail.get('productId')?.value;
    const quantity = detail.get('quantity')?.value;
    const productPrice = this.productPriceCache[productId] || 0;

    if (productPrice && quantity) {
      const totalPrice = quantity * productPrice;
      detail.get('totalPrice')?.setValue(totalPrice.toFixed(2));
    } else {
      detail.get('totalPrice')?.setValue('0.00');
    }
  }

  onProductChange(index: number): void {
    const detail = this.saleDetails.at(index) as FormGroup;
    const productId = detail.get('productId')?.value;
    const quantity = detail.get('quantity')?.value;

    if (productId && quantity) {
      this.productService.getProductById(productId).subscribe(
        product => {
          const unitPrice = product.unitPrice;
          const totalPrice = quantity * product.unitPrice;
          detail.get('unitPrice')?.setValue(unitPrice.toFixed(2));
          detail.get('totalPrice')?.setValue(totalPrice.toFixed(2));
        },
        error => console.error('Error fetching product price', error)
      );
    }
  }

  getProductPrice(detail: FormGroup): string {
    const productId = detail.get('productId')?.value;
    if (this.productPriceCache[productId]) {
      return this.productPriceCache[productId].toFixed(2);
    }

    this.productService.getProductById(productId).subscribe(product => {
      this.productPriceCache[productId] = product.unitPrice;
      detail.get('totalPrice')?.updateValueAndValidity();
    });

    return '0.00';
  }

  onDniChange(): void {
    const dni = this.saleForm.get('dni')?.value;

    if (dni) {
      this.customerService.getCustomerByDni(dni).subscribe(
        customer => {
          this.customerExists = !!customer; // Asignamos true si el cliente existe, false si no
          this.updateFormState();
        },
        error => {
          console.error('Error checking customer', error);
          this.customerExists = false; // En caso de error, asumimos que el cliente no existe
          this.updateFormState();
        }
      );
    } else {
      this.customerExists = false;
      this.updateFormState();
    }
  }

  updateFormState(): void {
    // Deshabilita el botón de agregar producto y el botón de crear venta si el cliente no existe
    const buttonsDisabled = !this.customerExists;
    this.saleDetails.controls.forEach(control => {
      (control as FormGroup).get('productId')?.disable({ onlySelf: buttonsDisabled, emitEvent: false });
      (control as FormGroup).get('quantity')?.disable({ onlySelf: buttonsDisabled, emitEvent: false });
    });

    this.saleForm.get('saleDetails')?.updateValueAndValidity();
  }

  startCustomerRegistration(): void {
    this.isRegisteringCustomer = true;
  }

  checkCustomer(): void {
    const dni = this.saleForm.get('dni')?.value;
    this.customerService.getCustomerByDni(dni).subscribe(customer => {
      this.customerExists = !!customer; // Asignamos true si el cliente existe, false si no
    }, error => {
      console.error('Error checking customer', error);
      this.customerExists = false; // En caso de error, asumimos que el cliente no existe
    });
  }

  onCustomerRegistered(): void {
    this.isRegisteringCustomer = false;
    this.onDniChange(); // Check if the customer is now registered
  }

  onSubmit(): void {
    if (this.saleForm.valid && this.customerExists) {
      const sale: Sale = {
        customerId: this.saleForm.get('dni')?.value,
        saleDate: new Date(),
        saleDetails: this.saleForm.get('saleDetails')?.value
      };

      this.saleService.addSale(sale).subscribe({
        next: response => {
          console.log('Successfully created sale', response);
          alert('Successfully created sale');
          this.saleForm.reset();
          this.customerExists = false; // Resetea el estado del cliente
        },
        error: err => {
          console.error('Error when creating the sale', err);
          alert('Error when creating the sale');
        }
      });
    } else {
      alert('Form is invalid or customer does not exist.');
    }
  }
}
