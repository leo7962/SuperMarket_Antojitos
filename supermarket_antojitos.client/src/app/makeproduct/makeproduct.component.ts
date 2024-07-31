import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ProductService } from '../services/product/product.service';
import { Product } from '../models/product.model';

@Component({
  selector: 'app-makeproduct',
  templateUrl: './makeproduct.component.html',
  styleUrl: './makeproduct.component.css'
})
export class MakeproductComponent implements OnInit {
  products: Product[] = [];
  productForm: FormGroup;

  constructor(private fb: FormBuilder, private productService: ProductService, private snackBar: MatSnackBar) {
    this.productForm = this.fb.group({
      productName: ['', Validators.required],
      productCode: ['', [Validators.required, Validators.pattern(/^\d+$/)]],
      unitPrice: ['', [Validators.required, Validators.pattern(/^\d+(\.\d{1,2})?$/)]],
      unitsInStock: ['', [Validators.required, Validators.pattern(/^\d+$/)]]
    });
  }

  ngOnInit(): void {
    this.productService.getProducts().subscribe((data: Product[]) => {
      this.products = data;
    })
  }

  onSubmit(): void {
    if (this.productForm.valid) {
      this.productService.addProduct(this.productForm.value).subscribe(
        response => {
          this.snackBar.open('Product added successfully!', 'Close', {
            duration: 3000,
            panelClass: ['snackbar-success']
          });
          // Reset the form after success
          this.productForm.reset();
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
