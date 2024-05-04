import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SeguridadService {

  constructor(private http:HttpClient) { }

  estaLogueado(): boolean { 
    return true;
  }
  obtenerRol():string{
    return 'admin';
  }
}
