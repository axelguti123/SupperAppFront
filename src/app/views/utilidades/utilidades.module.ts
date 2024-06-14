import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TablaDatosComponent } from './tabla-datos/tabla-datos.component';
import { FormsModule } from '@angular/forms';
import { ComboboxComponent } from './combobox/combobox.component';
import { FormFiltrosComponent } from './form-filtros/form-filtros.component';


@NgModule({
  declarations: [TablaDatosComponent, ComboboxComponent, FormFiltrosComponent],
  imports: [CommonModule,FormsModule,],
  exports: [TablaDatosComponent],
})
export class UtilidadesModule {}
