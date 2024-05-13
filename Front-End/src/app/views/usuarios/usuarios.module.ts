import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EspecialidadComponent } from './especialidad/especialidad.component';
import { FormEspecialidadComponent } from './form-especialidad/form-especialidad.component';
import { AppUsuariosModule } from './app-usuarios.module';
import { HttpClientModule } from '@angular/common/http';
import {
  ButtonModule,
  CardModule,
  CollapseModule,
  FormModule,
  GridModule,
} from '@coreui/angular';
import { MostrarErroresComponent } from '../utilidades/mostrar-errores/mostrar-errores.component';
import {UtilidadesModule} from '../utilidades/utilidades.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DataTablesModule } from 'angular-datatables';
import { UserComponent } from './user/user.component';

@NgModule({
  declarations: [
    EspecialidadComponent,
    FormEspecialidadComponent,
    MostrarErroresComponent,
    UserComponent,
  ],
  imports: [
    CommonModule,
    AppUsuariosModule,
    HttpClientModule,
    CardModule,
    CollapseModule,
    ButtonModule,
    GridModule,
    ReactiveFormsModule,
    FormModule,
    FormsModule,
    UtilidadesModule,
    DataTablesModule
    ],
})
export class UsuariosModule {}
