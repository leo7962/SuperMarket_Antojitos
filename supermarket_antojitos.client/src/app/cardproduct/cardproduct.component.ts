import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-cardproduct',
  templateUrl: './cardproduct.component.html',
  styleUrl: './cardproduct.component.css'
})
export class CardproductComponent {
  @Input() productName = null;


  constructor() {

    console.log("Estoy aqui")
    
  }

  ngOnInit(): void {
    console.log(this.productName);
  }
}
