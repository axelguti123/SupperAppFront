import { Component, EventEmitter, Input, Output } from '@angular/core';
import { especialidadDTO } from '../../../dto/especialidadDTO';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { ADTSettings } from 'angular-datatables/src/models/settings';
import { EspecialidadService } from '../../../services/especialidad.service';

@Component({
  selector: 'app-form-usuarios',
  templateUrl: './form-usuarios.component.html',
  styleUrl: './form-usuarios.component.scss'
})
export class FormUsuariosComponent {
  public visible = false;
  form: FormGroup;
  userControls: FormControl[] = [];
  @Output() headers: string[] = [];
  @Output()especialidadArray: any[];
  @Input() errores: string[] = [];
  @Input() especialidad: especialidadDTO;
  @Output() onSubmit = new EventEmitter<especialidadDTO>();
  constructor(
    private formBuilder: FormBuilder,
    private especialidadService: EspecialidadService,
  ) {}
  ngOnInit(): void {
   
    this.initializeForm();
    
  }
  handleLiveDemoChange(event: any) {
    this.visible = event;
  }
  initializeForm(): void {
    this.form = this.formBuilder.group({
      nombreEspecialidad  : [
        '',
        {
          validators: [Validators.required, Validators.minLength(3)],
        },
      ],
    });
    if (this.especialidad !== undefined) {
      this.form.patchValue(this.especialidad);
    }
  }
  toggleCollapse(id: number): void {
    this.visible[id] = !this.visible[id];
  }

  guardarCambios() {
    if (this.form.valid) {
      this.onSubmit.emit(this.form.value);
    }
  }
  toggleLiveDemo(){
    this.visible = !this.visible;
  }
  

  loadAllUser() {
    this.especialidadService.obtenerTodos().subscribe({
      next: (especialidad:any) => {
        this.especialidadArray = especialidad.data;
        console.log(this.especialidadArray);
      },
      error: (error) => console.error(error),
    });
  }
}
