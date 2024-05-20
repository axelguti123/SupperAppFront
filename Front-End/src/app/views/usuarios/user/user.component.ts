import { HttpClient } from '@angular/common/http';
import { Component, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { Config } from 'datatables.net';
import { Subject } from 'rxjs';
import { UsuarioService } from '../../../services/usuario.service';
import { UsuarioDTO } from '../../../dto/usuarioDTO';
import { FormBuilder, FormGroup } from '@angular/forms';
import { EspecialidadService } from '../../../services/especialidad.service';
import { especialidadDTO } from '../../../dto/especialidadDTO';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrl: './user.component.scss',
})
export class UserComponent implements OnInit, OnDestroy {
  userArray: any[]=[];
  especialidad: especialidadDTO[];
  dtOptions: Config = {};
  dtTrigger = new Subject();
  isChecked: boolean;
  constructor(private usuarioService: UsuarioService, private especialidadService:EspecialidadService) {}
  ngOnDestroy(): void {
    this.dtTrigger.unsubscribe();
  }

  ngOnInit(): void {
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 10,
    };
    this.loadAllEspecialidades();
    this.loadAllUser();
    
  }
  loadAllEspecialidades(){
    this.especialidadService.obtenerTodos().subscribe({
      next: (especialidades:any) => {
        this.especialidad = especialidades.data;
        console.log(especialidades)
      },
      error: (error) => console.error(error),
    });
  }
  loadAllUser() {
    this.usuarioService.obtenerTodos().subscribe({
      next: (usuario: any) => {
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
  Mod(data: any): void {
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
  validateField(item: any) {
    return !item.trim();
  }
  validateForm(obj: any) {
    return !obj.nombre || !obj.apellido || obj.nombreEspecialidad;
  }
  onCancel(item: any) {
    item.isEdit = false;
  }
  trackByFn(item: UsuarioDTO) {
    return item.idEspecialidad; // Usa una propiedad única del usuario si es posible
  }
  selectedUser: any;

  selectRow(user: any) {
    this.selectedUser = user;
  }
}
