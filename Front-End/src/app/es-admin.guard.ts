import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import {PermisosService} from './services/permisos.service'
export const esAdminGuard: CanActivateFn = (route, state) => {
  return inject(PermisosService).canActivate();
};
