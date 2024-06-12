import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { DefaultLayoutComponent } from './containers';
import { LoginComponent } from './views/pages/login/login.component';
import { esAdminGuard } from './es-admin.guard';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full',
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: '',
    component: DefaultLayoutComponent,
    canActivate: [esAdminGuard],
    data: {
      title: 'Home',
    },
    children: [
      {
        path: 'dashboard',
        loadChildren: () =>
          import('./views/dashboard/dashboard.module').then(
            (m) => m.DashboardModule
          ),
      },
      {
        path: 'especialidad',
        loadChildren: () =>
          import('./views/usuarios/usuarios.module').then((m) => m.UsuariosModule),
      },
      {
        path: 'partida',
        loadChildren: () =>
          import('./views/base/base.module').then((m) => m.BaseModule),
      },

      {
        path: 'pages',
        loadChildren: () =>
          import('./views/pages/pages.module').then((m) => m.PagesModule),
      },
    ],
  }, // Si ninguna ruta coincide, redirigir a la página de login
  { path: '**', redirectTo: 'login' },
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, {
      scrollPositionRestoration: 'top',
      anchorScrolling: 'enabled',
      initialNavigation: 'enabledBlocking',
      // relativeLinkResolution: 'legacy'
    }),
  ],
  exports: [RouterModule],
})
export class AppRoutingModule {}
