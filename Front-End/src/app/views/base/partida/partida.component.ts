import { Component } from '@angular/core';
import { especialidadDTO } from '../../../dto/especialidadDTO';
import { Config } from 'datatables.net';
import { Subject } from 'rxjs';
import { UsuarioDTO } from '../../../dto/usuarioDTO';
import { EspecialidadService } from '../../../services/especialidad.service';
import { UsuarioService } from '../../../services/usuario.service';

@Component({
  selector: 'app-partida',
  templateUrl: './partida.component.html',
  styleUrl: './partida.component.scss'
})
export class PartidaComponent {
  userArray: any[];
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

    this.loadAllUser();
    this.loadAllEspecialidades();
  }
  loadAllEspecialidades(){
    this.especialidadService.obtenerTodos().subscribe({
      next: (especialidades:any) => {
        this.especialidad = especialidades.data;
        console.log(this.especialidad)
      },
      error: (error) => console.error(error),
    });
  }
  loadAllUser() {
    this.usuarioService.obtenerTodos().subscribe({
      next: (usuario: any) => {
        this.userArray = usuario.data;
        console.log(this.userArray);
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
