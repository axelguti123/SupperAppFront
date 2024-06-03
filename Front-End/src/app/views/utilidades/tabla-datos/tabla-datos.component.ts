import { Component, Input, OnInit } from '@angular/core';
import { especialidadDTO } from '../../../dto/especialidadDTO';
import { UsuarioDTO } from '../../../dto/usuarioDTO';
import { PartidaDTO } from '../../../dto/partidaDTO';

@Component({
  selector: 'app-tabla-datos',
  templateUrl: './tabla-datos.component.html',
  styleUrl: './tabla-datos.component.scss',
})
export class TablaDatosComponent implements OnInit {
  @Input() listado: any[];
  @Input() headers: string[] = [];
  ngOnInit(): void {
    console.log(this.listado);
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

  selectRow(user: PartidaDTO) {
    this.selectedUser = user;
  }
}
