import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TablaDatosComponent } from './tabla-datos/tabla-datos.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ComboboxComponent } from './combobox/combobox.component';
import { FormFiltrosComponent } from './form-filtros/form-filtros.component';
import { DataTablesModule } from 'angular-datatables';


@NgModule({
  declarations: [TablaDatosComponent, ComboboxComponent, FormFiltrosComponent],
  imports: [CommonModule,FormsModule,DataTablesModule,ReactiveFormsModule],
  exports: [FormFiltrosComponent,TablaDatosComponent],
})
export class UtilidadesModule {}
