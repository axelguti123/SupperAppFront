import { Component, Input, OnInit } from '@angular/core';
import {SeguridadService} from '../../services/seguridad.service'
@Component({
  selector: 'app-autorizado',
  templateUrl: './autorizado.component.html',
  styleUrl: './autorizado.component.scss'
})
export class AutorizadoComponent implements OnInit{
  @Input()
  cargo: string='';
  ngOnInit(): void {
    
  }
  

  constructor(private seguridadService: SeguridadService){

  }
  estaAutorizado(): boolean {
    if(this.cargo){
      return this.seguridadService.obtenerRol()===this.cargo
    }else{
      return this.seguridadService.estaLogueado();
    }
      
  }
}
