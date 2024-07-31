import { Component, Input } from '@angular/core';
import { Product } from '../models/product.model';

@Component({
  selector: 'app-cardproduct',
  templateUrl: './cardproduct.component.html',
  styleUrl: './cardproduct.component.css'
})
export class CardproductComponent {
  @Input() product?: Product;

  constructor() {
    
  }
}
