import { Component, OnDestroy, OnInit } from '@angular/core';
import { Config } from 'datatables.net';
import { Subject, takeUntil, tap } from 'rxjs';
import { UsuarioService } from '../../../services/usuario.service';
import { UsuarioDTO } from '../../../dto/usuarioDTO';
import { EspecialidadService } from '../../../services/especialidad.service';
import { especialidadDTO } from '../../../dto/especialidadDTO';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';
@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrl: './user.component.scss',
})
export class UserComponent implements OnInit, OnDestroy {
  userArray: UsuarioDTO[] = [];
  especialidad: especialidadDTO[] = [];
  dtOptions: Config = {};
  dtTrigger = new Subject<Config>();
  isChecked: boolean = false;
  private unsubscribe$ = new Subject<void>();
  userForm: FormGroup;
  constructor(
    private usuarioService: UsuarioService,
    private especialidadService: EspecialidadService,
    private fb: FormBuilder
  ) {
    this.userForm = this.fb.group({
      users: this.fb.array([]),
    });
  }
  ngOnDestroy(): void {
    this.dtTrigger.unsubscribe();
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  ngOnInit(): void {
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 10,
    };

    this.loadAllUser();
    this.loadAllEspecialidades();
  }
  loadAllEspecialidades() {
    this.especialidadService
      .obtenerTodos()
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe({
        next: (especialidades: {
          data: especialidadDTO[];
          message: string;
          status: string;
        }) => {
          this.especialidad = especialidades.data;
          console.log(especialidades);
        },
        error: (error) => console.error(error),
      });
  }
  loadAllUser() {
    const startTime = performance.now();
    this.usuarioService
      .obtenerTodos()
      .pipe(
        tap(() => {
          const endTime = performance.now();
          console.log(
            `Load all specialties took ${endTime - startTime} milliseconds.`
          );
        }),
        takeUntil(this.unsubscribe$)
      )
      .subscribe({
        next: (usuario: {
          data: UsuarioDTO[];
          message: string;
          status: string;
        }) => {
          const usersArray=usuario.data.map(user=>this.createUser(user));
          this.userForm.setControl('users',this.fb.array(usersArray))
          this.dtTrigger.next(this.dtOptions);
        },
        error: (error) => console.error(error),
      });
  }
  editStates = {};
  createUser(data: any): FormGroup {
    const userForm = this.fb.group({
      nombre: [data.nombre],
      apellido: [data.apellido],
      idEspecialidad: [data.idEspecialidad],
      isActivo: [data.isActivo]
    });
    this.editStates[data.nombre] = {
      nombre: false,
      apellido: false,
      idEspecialidad: false
    };
    return userForm;
  }
  get users() {
    return this.userForm.get('users') as FormArray;
  }
  Mod(data: UsuarioDTO): void {
    data.isActivo = !data.isActivo;
    console.log(data);
  }
  originalValue: any;
  onEdit(index: number, field: string): void {
    const user = this.users.at(index).value.nombre;
    this.editStates[user][field] = true;
  }
  isEdit(index: number, field: string): boolean {
    const user = this.users.at(index).value.nombre;
    return this.editStates[user][field];
  }
  selectedUser: any;
  
  selectRow(user: any) {
    this.selectedUser = user;
  }
  onUpdate(data: any): void {
    console.log(data);
  }
  onRowUpdate(index: number): void {
    const user = this.users.at(index).value.nombre;
    console.log('Datos actualizados:', this.users.at(index).value);
    Object.keys(this.editStates[user]).forEach(field => {
      this.editStates[user][field] = false;
    });
  }
  validateField(item: any): boolean {
    return !item;
  }
  validateForm(obj: any): boolean {
    return !obj.nombre || !obj.apellido || obj.nombreEspecialidad;
  }
  onCancel(item: any) {
    item.isEdit = false;
  }
  trackByFn(index: number, item: UsuarioDTO): number {
    return item.idEspecialidad; // Usa una propiedad única del usuario si es posible
  }
 
  toggleActivo(index: number): void {
    const isActive = this.users.at(index).get('isActivo').value;
    this.users.at(index).get('isActivo').setValue(!isActive);
    this.onRowUpdate(index);
  }

  getEspecialidadName(id: number): string {
    console.log(id)
    const especialidad = this.especialidad.find(e => e.idEspecialidad === id);
    
    return especialidad ? especialidad.NombreEspecialidad : 'Desconocida';
  }
}
