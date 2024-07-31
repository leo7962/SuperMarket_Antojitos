import { Component, OnInit } from '@angular/core';
import { Product } from '../models/product.model';
import { ProductService } from '../services/product/product.service';


@Component({  
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrl: './products.component.css',
  
})
export class ProductsComponent implements OnInit {
  products: Product[] = [];

  constructor(private productService: ProductService) {

  }

  ngOnInit(): void {
    this.productService.getProducts().subscribe(
      (products) => this.products = products,
      (error) => console.error('Error loading products', error)
    );
  }
  
}
