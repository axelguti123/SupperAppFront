import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {AutorizadoComponent} from './autorizado/autorizado.component'


@NgModule({
  declarations: [AutorizadoComponent],
  imports: [
    CommonModule
  ],
  exports:[AutorizadoComponent]
})
export class SeguridadModule { }
