import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TablaDatosComponent } from './tabla-datos/tabla-datos.component';
import { FormsModule } from '@angular/forms';


@NgModule({
  declarations: [TablaDatosComponent],
  imports: [CommonModule,FormsModule,],
  exports: [TablaDatosComponent],
})
export class UtilidadesModule {}
