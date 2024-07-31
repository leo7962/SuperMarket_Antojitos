import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ReportService {
  private apiUrl = 'http://localhost:5268/api/Reports'

  constructor(private http: HttpClient) { }

  getSalesReportExcel(startDate: string, endDate: string): Observable<Blob> {
    const url = `${this.apiUrl}/export/excel?startDate=${startDate}&endDate=${endDate}`;
    return this.http.get(url, { responseType: 'blob' });
  }

  getSalesReportPdf(startDate: string, endDate: string): Observable<Blob> {
    const url = `${this.apiUrl}/export/pdf?startDate=${startDate}&endDate=${endDate}`;
    return this.http.get(url, { responseType: 'blob' });
  }
}
