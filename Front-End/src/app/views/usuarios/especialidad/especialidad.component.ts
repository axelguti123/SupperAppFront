import { Component, Input, OnInit } from '@angular/core';
import { EspecialidadService } from '../../../services/especialidad.service';
import { especialidadDTO } from '../../../dto/especialidadDTO';
import { Router } from '@angular/router';
import { parsearErroresAPI } from '../../utilidades';

@Component({
  selector: 'app-especialidad',
  templateUrl: './especialidad.component.html',
  styleUrl: './especialidad.component.scss',
})
export class EspecialidadComponent implements OnInit {
  constructor(
    private especialidadService: EspecialidadService,
    private router: Router
  ) {}
  
  errores: string[] = [];
  especialidad: especialidadDTO[];
  ngOnInit(): void {
    this.traerTabla();
  }

  traerTabla(){
    this.especialidadService.obtenerTodos().subscribe({
      next: (especialidad:any) => {
        this.especialidad = especialidad.data;
        console.log(especialidad)
      },
      error: (error) => console.error(error),
    });
  }
  guardarCambios(especialidad: especialidadDTO) {
    
    this.especialidadService.crear(especialidad).subscribe({
      next: () => {
        this.router.navigate(['/especialidad']);
        this.traerTabla();
        
      },
      error: (error) => (this.errores = parsearErroresAPI(error)),
    });
    

  }
}
