import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { especialidadDTO } from '../../../dto/especialidadDTO';
import { EspecialidadService } from '../../../services/especialidad.service';
import { ADTSettings } from 'angular-datatables/src/models/settings';
@Component({
  selector: 'app-form-especialidad',
  templateUrl: './form-especialidad.component.html',
  styleUrl: './form-especialidad.component.scss',
})
export class FormEspecialidadComponent implements OnInit {
  dtOptions: ADTSettings = {};
  visible: boolean[] = [false, false];
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
    this.dtOptions = {
      pagingType: 'full_numbers'
    };
    this.loadAllUser();
    this.initializeForm();
    
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
  
  
  validateUserName(userName: string) {
    if (userName === '') {
      return 'Required';
    } else {
      if (userName.length >= 3) {
        return '';
      } else {
        return 'min 3 char';
      }
    }
  }

  loadAllUser() {
    this.especialidadService.obtenerTodos().subscribe({
      next: (especialidad:especialidadDTO[]) => {
        this.especialidadArray = especialidad;
        console.log(this.especialidadArray);
      },
      error: (error) => console.error(error),
    });
  }
  filtro:String='';
  validateForm(obj: especialidadDTO): boolean {
    return obj.NombreEspecialidad !== '' && obj.estado;
  }
  validateField(item: any) {
    if (item !== '') {
      return false;
    } else {
      return true;
    }
  }
  onCancel(item: any) {
    item.isEdit = false;
  }
  trackByFn(index: number, item: any) {
    return item.id; // Usa una propiedad única del usuario si es posible
  }
  selectedUser: any;

  selectRow(user: any) {
    this.selectedUser = user;
  }
  originalValue: any;
  onEdit(item: any) {
    this.originalValue = { ...item };
    item.isEdit = true;
  }
  onUpdate(data: any): void {
    console.log(data);
  }
  onRowUpdate(user: any): void {
    if (
      this.originalValue &&
      JSON.stringify(user) !== JSON.stringify(this.originalValue)
    ) {
      // El valor ha cambiado
      this.onUpdate(user);
    }
    user.isEdit = !user.isEdit;
  }
}
