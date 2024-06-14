import { AfterViewInit, Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Config } from 'datatables.net';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-form-filtros',
  templateUrl: './form-filtros.component.html',
  styleUrl: './form-filtros.component.scss'
})
export class FormFiltrosComponent implements AfterViewInit,OnDestroy,OnInit{
  
  dtOptions: Config = {};
  dtTrigger = new Subject<Config>();

  @ViewChild('datatable', { static: true }) table: ElementRef;
  private dataTable: any;
  ngOnInit(): void {
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 10,
      scrollX:true
    };
  }
  ngAfterViewInit(): void {
    
  }
  ngOnDestroy(): void {
    if (this.dataTable) {
      this.dataTable.destroy();
    }
    this.dtTrigger.unsubscribe();
  }
  
}
