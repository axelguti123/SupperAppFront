import { Component, EventEmitter, Input, OnInit, Output, input } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { especialidadDTO } from '../../../dto/especialidadDTO';
import { Config } from 'datatables.net';
import { Subject } from 'rxjs';
@Component({
  selector: 'app-form-especialidad',
  templateUrl: './form-especialidad.component.html',
  styleUrl: './form-especialidad.component.scss',
})
export class FormEspecialidadComponent implements OnInit {
  @Input() editStates = {};
  @Input() dtOptions: Config = {};
  @Input() dtTrigger= new Subject<Config>();
  visible: boolean[] = [false, false];
  @Input() form:FormGroup;
  @Input() forms: FormGroup;  
  userControls: FormControl[] = [];
  @Output() headers: string[] = [];
  @Output()especialidadArray: any[];
  @Input() errores: string[] = [];
  @Input() especialidad: especialidadDTO;
  @Output() onSubmit = new EventEmitter<especialidadDTO>();
  constructor(
    private formBuilder: FormBuilder,
  ) {}
  ngOnInit(): void {
    this.initializeForm();
    console.log(this.forms)
    
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
  get list(): FormArray{
    return this.forms.get('list') as FormArray
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
  filtro:String='';
  validateForm(obj: especialidadDTO): boolean {
    return obj.nombreEspecialidad !== '' && obj.estado;
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
  trackByFn(index: number, item: FormGroup): number {
    console.log(index)
    return item.value.idPartida; // Usa una propiedad única del usuario si es posible
  }
  onEdit(index: number, field: string): void {
    const partida = this.list.at(index).value.idEspecialidad;
    this.editStates[partida][field] = true;
  }
  isEdit(index: number, field: any): boolean {
    const user = this.list.at(index).value.idEspecialidad;
    console.log(user);
    return this.editStates[user] ? this.editStates[user][field] : false;
  }
  onUpdate(data: any): void {
    console.log(data);
  }
}
