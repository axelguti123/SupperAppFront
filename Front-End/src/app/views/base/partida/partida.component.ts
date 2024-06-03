import { Component, OnDestroy, OnInit } from '@angular/core';
import { Config } from 'datatables.net';
import { Subject, takeUntil } from 'rxjs';
import { UsuarioDTO } from '../../../dto/usuarioDTO';
import { PartidaDTO } from '../../../dto/partidaDTO';
import { PartidaService } from '../../../services/partida.service';

@Component({
  selector: 'app-partida',
  templateUrl: './partida.component.html',
  styleUrl: './partida.component.scss',
})
export class PartidaComponent implements OnInit, OnDestroy {
  partidaArray: PartidaDTO[] = [];
  dtOptions: Config = {};
  dtTrigger = new Subject<Config>();
  isChecked: boolean = false;
  private unsubscribe$ = new Subject<void>();
  constructor(
    private partidaService: PartidaService
  ) {}
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

    this.loadAllPartida();
  }
  loadAllPartida() {
    this.partidaService
      .obtenerTodos()
      .pipe(
        takeUntil(this.unsubscribe$)
      )
      .subscribe({
        next: (usuario: {
          data: PartidaDTO[];
          message: string;
          status: string;
        }) => {
          this.partidaArray = usuario.data;
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
    return !item.trim();
  }
  validateForm(obj: any): boolean {
    return !obj.nombre || !obj.apellido || obj.nombreEspecialidad;
  }
  onCancel(item: any) {
    item.isEdit = false;
  }
  trackByFn(index: number, item: UsuarioDTO) {
    return item.idEspecialidad; // Usa una propiedad única del usuario si es posible
  }
  selectedUser: any;

  selectRow(user: any) {
    this.selectedUser = user;
  }
}
