import { SaleDetail } from "./saledetail.model";

export interface Sale {
  saleDate: Date;
  customerId: number;
  saleDetails: SaleDetail[];
}
