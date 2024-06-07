import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import {SeguridadService} from './seguridad.service'

@Injectable({
  providedIn: 'root'
})
export class PermisosService {

  constructor(private seguridadServices:SeguridadService,private router: Router) { }

  canActivate():boolean {
    if(this.seguridadServices.obtenerRol()==='admin'){
      return true;
    }

     this.router.navigate(['/login'])
     return false
  }
}
