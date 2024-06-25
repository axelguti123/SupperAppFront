import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

// CoreUI Modules
import {
  ButtonModule,
  CarouselModule,
  DropdownModule,
  FormModule,
  NavModule,
  PaginationModule,
  PopoverModule,
  TableModule,
  TabsModule,
  TooltipModule,
} from '@coreui/angular';

// views
import { SeguimientoComponent } from './seguimiento/seguimiento.component';
import { PartidaComponent } from './partida/partida.component';
import { TabsComponent } from './tabs/tabs.component';
import { SeguridadModule } from '../../seguridad/seguridad.module'

// Components Routing
import { BaseRoutingModule } from './base-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { UtilidadesModule } from '../utilidades/utilidades.module';
import { DataTablesModule } from 'angular-datatables';
@NgModule({
  imports: [
    CommonModule,
    BaseRoutingModule,
    TabsModule,
    NavModule,
    TooltipModule,
    CarouselModule,
    ReactiveFormsModule,
    DropdownModule,
    PopoverModule,
    TableModule,
    FormsModule,
    HttpClientModule,
    SeguridadModule,
    UtilidadesModule,
    DataTablesModule,
    FormModule,
    ButtonModule
  ],
  declarations: [
    SeguimientoComponent,
    TabsComponent,
    PartidaComponent,
  ],
})
export class BaseModule {}
