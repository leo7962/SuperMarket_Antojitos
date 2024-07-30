import { CommonModule } from '@angular/common';
import { Component, NgModule } from '@angular/core';


@Component({  
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrl: './products.component.css',
  
})
export class ProductsComponent {
  products: any = [
    {
      "productId": 1,
      "productCode": "Celular",
      "productName": "Samsung Galaxy 21 FE",
      "unitPrice": 1250,
      "unitsInStock": 15
    }
  ]
}
