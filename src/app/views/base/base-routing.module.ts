import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { SeguimientoComponent } from './seguimiento/seguimiento.component';
import { TabsComponent } from './tabs/tabs.component';
import {esAdminGuard} from '../../es-admin.guard'
const routes: Routes = [
  {
    path: '',
    data: {
      title: 'Partida',
    },
    children: [
      {
        path: '',
        pathMatch: 'full',
        redirectTo: 'partida',
      },
      {
        path:'partida',
        component:TabsComponent,
        canActivate:[esAdminGuard],
        data:{
          title: 'Partida',
        },
        
      },
      {
        path:'seguimiento',
        component:SeguimientoComponent,
        canActivate:[esAdminGuard],
        data:{
          title: 'Seguimiento',
        }
      }
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class BaseRoutingModule {}

