import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CommonModule } from '@angular/common';
import {esAdminGuard} from '../../es-admin.guard'
import { EspecialidadComponent } from './especialidad/especialidad.component';

const routes: Routes = [
  {
    path: '',
    data: {
      title: 'Profesional',
    },
    children: [
      {
        path: '',
        pathMatch: 'full',
        redirectTo: 'especialidad',
      },
      {
        path: 'especialidad',
        component: EspecialidadComponent,
        canActivate:[esAdminGuard],
        data:{
          title: 'especialidad',
        }
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AppUsuariosModule {}
