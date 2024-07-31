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
      dni: ['', [Validators.required, Validators.pattern(/^\d{10}$/)]],
      saleDetails: this.fb.array([])
    });
  }

  ngOnInit(): void {
    this.productService.getProducts().subscribe(products => this.products = products);
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

    console.log('Product ID:', productId); // Debugging line
    console.log('Quantity:', quantity);   // Debugging line

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

    // Si el precio no está en caché, obtén el precio del servicio
    this.productService.getProductById(productId).subscribe(product => {
      this.productPriceCache[productId] = product.unitPrice;
      detail.get('totalPrice')?.updateValueAndValidity(); // Recalcula el total si es necesario
    });

    return '0.00';
  }

  onDniChange(): void {
    const dni = Number(this.saleForm.get('dni')?.value);
    console.log('DNI captured:', dni);

    if (dni) {
      this.customerService.getCustomerByDni(dni).subscribe(
        customer => {
          if (customer) {
            this.customerExists = true;
            this.isRegisteringCustomer = false;
          } else {
            this.customerExists = false;
            this.isRegisteringCustomer = false;
          }
        },
        error => console.error('Error checking customer', error)
      );
    } else {
      console.error('DNI is empty or invalid');
    }
  }

  startCustomerRegistration(): void {
    this.isRegisteringCustomer = true;
  }

  onCustomerRegistered(): void {
    this.isRegisteringCustomer = false;
    this.onDniChange(); // Check if the customer is now registered
  }

  onSubmit(): void {
    if (this.saleForm.valid) {
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
        },
        error: err => {
          console.error('Error when creating the sale', err);
          alert('Error when creating the sale');
        }
      });
    }
  }
}
