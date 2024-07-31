import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CustomerComponent } from './customer/customer.component';
import { NavbarComponent } from './navbar/navbar.component';
import { ProductsComponent } from './products/products.component';
import { CardproductComponent } from './cardproduct/cardproduct.component';
import { MakeproductComponent } from './makeproduct/makeproduct.component';
import { ReactiveFormsModule } from '@angular/forms';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { CreatesaleComponent } from './createsale/createsale.component';
import { RegistercustomerComponent } from './registercustomer/registercustomer.component';

@NgModule({
  declarations: [
    AppComponent,
    CustomerComponent,
    NavbarComponent,
    ProductsComponent,
    CardproductComponent,
    MakeproductComponent,
    CreatesaleComponent,
    RegistercustomerComponent
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule, ReactiveFormsModule
  ],
  providers: [
    provideAnimationsAsync()
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
