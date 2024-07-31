import { Component, OnInit } from '@angular/core';
import { ReportService } from '../services/report/report.service';
import { saveAs } from 'file-saver';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrl: './report.component.css'
})
export class ReportComponent implements OnInit {
  reportForm!: FormGroup;

  constructor(private fb: FormBuilder, private reportService: ReportService) { }

  ngOnInit(): void {
    this.reportForm = this.fb.group({
      startDate: [''],
      endDate: ['']
    });
  }

  generateReport(format: 'excel' | 'pdf'): void {
    const startDate = this.reportForm.get('startDate')?.value;
    const endDate = this.reportForm.get('endDate')?.value;

    if (!startDate || !endDate) {
      alert('Please select both start and end dates.');
      return;
    }

    const start = encodeURIComponent(startDate);
    const end = encodeURIComponent(endDate);

    if (format === 'excel') {
      this.reportService.getSalesReportExcel(start, end).subscribe(response => {
        saveAs(response, 'SalesReport.xlsx');
      }, error => {
        console.error('Error generating Excel report', error);
      });
    } else if (format === 'pdf') {
      this.reportService.getSalesReportPdf(start, end).subscribe(response => {
        saveAs(response, 'SalesReport.pdf');
      }, error => {
        console.error('Error generating PDF report', error);
      });
    }
  }
}
