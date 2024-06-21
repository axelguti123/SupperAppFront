import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TablaDatosComponent } from './tabla-datos/tabla-datos.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ComboboxComponent } from './combobox/combobox.component';
import { FormFiltrosComponent } from './form-filtros/form-filtros.component';
import { DataTablesModule } from 'angular-datatables';
import { ButtonComponent } from './button/button.component';
import { ButtonModule } from '@coreui/angular';


@NgModule({
  declarations: [TablaDatosComponent, ComboboxComponent, FormFiltrosComponent, ButtonComponent],
  imports: [CommonModule,FormsModule,DataTablesModule,ReactiveFormsModule,ButtonModule],
  exports: [FormFiltrosComponent,TablaDatosComponent,ButtonComponent],
})
export class UtilidadesModule {}
