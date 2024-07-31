import { Injectable } from '@angular/core';
import { Sale } from '../../models/sale.model';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SaleService {
  private apirUrl = 'http://localhost:5268/api/Sales';
  constructor(private http: HttpClient) { }

  getSales(): Observable<Sale[]> {
    return this.http.get<Sale[]>(this.apirUrl);
  }

  getSaleById(id: number): Observable<Sale> {
    return this.http.get<Sale>(`${this.apirUrl}/${id}`);
  }

  addSale(sale: Sale): Observable<Sale> {
    return this.http.post<Sale>(this.apirUrl, sale);
  }

  updateSale(id: number, sale: Sale): Observable<Sale> {
    return this.http.put<Sale>(`${this.apirUrl}/${id}`, sale);
  }

  deleteSale(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apirUrl}/${id}`);
  }
}
