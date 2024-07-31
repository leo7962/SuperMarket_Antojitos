import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CustomerComponent } from './customer/customer.component';
import { MakeproductComponent } from './makeproduct/makeproduct.component';
import { ProductsComponent } from './products/products.component';
import { CreatesaleComponent } from './createsale/createsale.component';
import { ReportComponent } from './report/report.component';


const routes: Routes = [
  { path: '', component: ProductsComponent },
  { path: 'make-product', component: MakeproductComponent },
  { path: 'create-costumer', component: CustomerComponent },
  { path: 'create-sale', component: CreatesaleComponent },
  { path: 'report', component: ReportComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
