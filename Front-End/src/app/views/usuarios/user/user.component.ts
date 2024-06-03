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
  userForm:FormGroup;
  constructor(
    private usuarioService: UsuarioService,
    private especialidadService: EspecialidadService,
    private fb:FormBuilder
  ) {
    this.userForm = fb.group({
      users:this.fb.group({
        idUsuario:'',
        nombre:'',
        apellido:'',
        idEspecialidad:'',
        nombreEspecialidad:'',
        isActivo:''
      })
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
  initializeform(){
    const usersArray=this.userForm.get('users') as FormArray;
    this.userArray.forEach(user =>{
      
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
          this.userArray = usuario.data;
          console.log(usuario);
          this.dtTrigger.next(this.dtOptions);
        },
        error: (error) => console.error(error),
      });
  }
  originalValue: any;
  onEdit(item: any) {
    this.originalValue = { ...item };
    item.isEdit = true;
  }
  onUpdate(data: any): void {
    console.log(data);
  }
  Mod(data: UsuarioDTO): void {
    data.isActivo = !data.isActivo;
    console.log(data);
  }
  onRowUpdate(user: any): void {
    if (
      this.originalValue &&
      JSON.stringify(user) !== JSON.stringify(this.originalValue)
    ) {
      // El valor ha  cambiado
      this.onUpdate(user);
    }
    user.isEdit = !user.isEdit;
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
  trackByFn(index: number, item: UsuarioDTO):number {
    return item.idEspecialidad; // Usa una propiedad única del usuario si es posible
  }
  selectedUser: any;

  selectRow(user: any) {
    this.selectedUser = user;
    console.log(this.selectedUser);
  }
  onSpecialidadChange(user: any, event: any): void {
    const selectedIdEspecialidad = event.target.value;
    user.idEspecialidad = selectedIdEspecialidad;
    const selectedEspecialidad = this.especialidad.find(e => e.idEspecialidad === selectedIdEspecialidad);
    console.log('Selected User:', user);
    console.log('Selected Especialidad:', selectedEspecialidad);
  }
}
