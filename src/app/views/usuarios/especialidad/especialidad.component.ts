import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { EspecialidadService } from '../../../services/especialidad.service';
import { especialidadDTO } from '../../../dto/especialidadDTO';
import { Router } from '@angular/router';
import { parsearErroresAPI } from '../../utilidades';
import { Config } from 'datatables.net';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-especialidad',
  templateUrl: './especialidad.component.html',
  styleUrl: './especialidad.component.scss',
})
export class EspecialidadComponent implements OnInit,OnDestroy {
  dtOptions: Config = {};
  especialidadForm: FormGroup;
  editStates: { [key: string]: any } = {};
  dtTrigger = new Subject<Config>();

  constructor(
    private especialidadService: EspecialidadService,
    private router: Router,
    private fb: FormBuilder
  ) {
    this.especialidadForm = fb.group({
      list: this.fb.array([]),
    });
  }
  ngOnDestroy(): void {
    this.dtTrigger.unsubscribe();
  }
  private initializeDataTableOptions(): void {
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 10,
      columns: [
        { title: 'Nro' },
        { title: 'Especialidad' },
        { title: 'Delete', orderable: false, searchable: false },
      ],
    };
  }
  errores: string[] = [];
  especialidad: especialidadDTO[];
  ngOnInit(): void {
    this.initializeDataTableOptions();
    this.traerTabla();
  }

  traerTabla() {
    this.especialidadService.obtenerTodos().subscribe({
      next: (especialidad: any) => {
        this.InitializeForm(especialidad.data);
        console.log(especialidad.data);
        this.dtTrigger.next(this.dtOptions);
      },
      error: (error) => console.error(error),
    });
  }
  InitializeForm(especialidad: especialidadDTO[]): void {
    const especialidadArray=especialidad.map((especialidad)=>this.createEspecialidad(especialidad))
    this.especialidadForm.setControl('list',this.fb.array(especialidadArray))
  }
  createEspecialidad(especialidad:especialidadDTO):FormGroup{
    const especialidadForm=this.fb.group({
      idEspecialidad:[especialidad.idEspecialidad],
      nombreEspecialidad:[especialidad.nombreEspecialidad]
    })
    this.editStates[especialidad.idEspecialidad]={
      nombreEspecialidad:false
    }
    return especialidadForm;
  }
  get list(){
    return this.especialidadForm.get('list') as FormArray
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
