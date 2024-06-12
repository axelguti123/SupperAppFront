import {
  ChangeDetectionStrategy,
  Component,
  OnDestroy,
  OnInit,
  ChangeDetectorRef,
} from '@angular/core';
import { Config } from 'datatables.net';
import { Subject, Subscription, takeUntil, tap } from 'rxjs';
import { UsuarioService } from '../../../services/usuario.service';
import { UsuarioDTO } from '../../../dto/usuarioDTO';
import { EspecialidadService } from '../../../services/especialidad.service';
import { especialidadDTO } from '../../../dto/especialidadDTO';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { parsearErroresAPI } from '../../utilidades';
@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrl: './user.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class UserComponent implements OnInit, OnDestroy {
  especialidad: especialidadDTO[] = [];
  dtOptions: Config = {};
  dtTrigger = new Subject<Config>();
  isChecked: boolean = false;
  private unsubscribe$ = new Subject<void>();
  userForm: FormGroup;
  newUserForm:FormGroup;
  constructor(
    private usuarioService: UsuarioService,
    private especialidadService: EspecialidadService,
    private fb: FormBuilder,
    private ref: ChangeDetectorRef,
    private router: Router
  ) {
    this.userForm = fb.group({
      users: this.fb.array([]),
    });
    this.newUserForm = this.fb.group({
      idUsuario: [''],
      nombre: ['', Validators.required],
      apellido: ['', Validators.required],
      idEspecialidad: ['', Validators.required],
      nombreEspecialidad: [''],
      isActivo: [false],
    });
  
  }
  ngOnDestroy(): void {
    this.dtTrigger.unsubscribe();
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
    this.unsubscribeFromData();
  }
  ngOnInit(): void {
    this.loadAllEspecialidades();
    this.subscribeData();
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 10,
      scrollX:true
    };
  }
  
  private unsubscribeFromData(): void {
    // Verifica si hay una suscripción activa y la cierra
    if (this.dataSubscription && !this.dataSubscription.closed) {
      this.dataSubscription.unsubscribe();
    }
  }
  private dataSubscription: Subscription;
  private subscribeData(): void {
    if (!this.dataSubscription || this.dataSubscription.closed) {
      const startTime = performance.now();
      this.dataSubscription = this.usuarioService
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
            const usersArray = usuario.data.map((user) =>
              this.createUser(user)
            );
            console.log(usersArray)
            this.userForm.setControl('users', this.fb.array(usersArray));
            this.dtTrigger.next(this.dtOptions);
            this.ref.markForCheck();
          },
          error: (error) => console.error(error),
        });
    }
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
          this.ref.markForCheck();
          console.log(especialidades);
        },
        error: (error) => console.error(error),
      });
  }
  editStates = {};
  createUser(data: UsuarioDTO): FormGroup {
    const userForm = this.fb.group({
      idUsuario: [data.idUsuario],
      nombre: [data.nombre],
      apellido: [data.apellido],
      idEspecialidad: [data.idEspecialidad],
      nombreEspecialidad: [data.nombreEspecialidad],
      isActivo: [data.isActivo],
    });
    this.editStates[data.idUsuario] = {
      nombre: false,
      apellido: false,
      idEspecialidad: false,
    };
    return userForm;
  }
  addNewUser(){
    const newUser= this.fb.group({
      idUsuario: [''],
      nombre: [''],
      apellido: [''],
      idEspecialidad: [''],
      nombreEspecialidad: [''],
      isActivo:[false]
    });
    this.users.push(newUser);
    this.ref.markForCheck();
  }
  get users():FormArray {
    return this.userForm.get('users') as FormArray;
  }
  onEdit(index: number, field: string, event: Event): void {
    event.stopPropagation();
    const user = this.users.at(index).value.idUsuario;
    this.editStates[user][field] = true;
  }
  isEdit(index: number, field: any): boolean {
    const user = this.users.at(index).value.idUsuario;
    console.log(user);
    return this.editStates[user] ? this.editStates[user][field] : false;
  }
  onUpdate(data: UsuarioDTO): void {
    this.usuarioService.update(data).subscribe();
  }
  onRowUpdate(index: number): void {
    const user = this.users.at(index).value.idUsuario;
    const data = this.users.at(index).value;
    console.log('Datos actualizados:', data);
    this.onUpdate(data);
    Object.keys(this.editStates[user]).forEach((field) => {
      this.editStates[user][field] = false;
    });
  }
  onCreate(){
    
  }
  trackByFn(item: UsuarioDTO): number {
    return item.idEspecialidad; // Usa una propiedad única del usuario si es posible
  }

  toggleActivo(index: number): void {
    const isActive = this.users.at(index).get('isActivo').value;
    this.users.at(index).get('isActivo').setValue(!isActive);
    this.onRowUpdate(index);
  }
  ModCombobox(index: number): void {
    const idEspecialidad = this.users.at(index).get('idEspecialidad').value;
    this.users.at(index).get('idEspecialidad').setValue(idEspecialidad);
    this.onRowUpdate(index);
  }
  guardarCambios(especialidad: especialidadDTO) {
    
    this.especialidadService.crear(especialidad).subscribe({
      next: () => {
        this.router.navigate(['/especialidad']);
        
        
      },
      error: (error) => console.log(error),
    });
    

  }
}
