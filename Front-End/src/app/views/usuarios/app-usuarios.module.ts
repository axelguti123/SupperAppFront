import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {esAdminGuard} from '../../es-admin.guard'
import { EspecialidadComponent } from './especialidad/especialidad.component';
import { UserComponent } from './user/user.component';

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
      {
        path:'user',
        component:UserComponent,
        canActivate:[esAdminGuard],
        data:{
          title:'user'
        }
      }
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AppUsuariosModule {}
