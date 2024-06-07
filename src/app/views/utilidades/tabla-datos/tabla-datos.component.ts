import { Component, Input, OnInit } from '@angular/core';
import { especialidadDTO } from '../../../dto/especialidadDTO';

@Component({
  selector: 'app-tabla-datos',

  templateUrl: './tabla-datos.component.html',
  styleUrl: './tabla-datos.component.scss',
})
export class TablaDatosComponent implements OnInit {
  @Input() listado: any[];
  @Input() headers: string[] = [];
  ngOnInit(): void {
    console.log(this.listado);
  }
  
}
