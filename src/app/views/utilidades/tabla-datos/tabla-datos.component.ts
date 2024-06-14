import { Component, EventEmitter, Input, OnDestroy, OnInit, Output, input } from '@angular/core';
import { especialidadDTO } from '../../../dto/especialidadDTO';
import { UsuarioDTO } from '../../../dto/usuarioDTO';
import { PartidaDTO } from '../../../dto/partidaDTO';
import { Config } from 'datatables.net';
import { Subject } from 'rxjs';
import { FormArray, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-tabla-datos',
  templateUrl: './tabla-datos.component.html',
  styleUrl: './tabla-datos.component.scss',
})
export class TablaDatosComponent implements OnInit,OnDestroy {
  ngOnDestroy(): void {
    
  }
  @Input() forms: FormGroup;
  @Input() data: FormArray;
  @Input() editStates= {}
  @Output() onRowUpdate=new EventEmitter<number>();
  dtOptions: Config = {};
  dtTrigger = new Subject<Config>();
  get list(): FormArray {
    return this.forms.get('list') as FormArray;
  }
  ngOnInit(): void {
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 10,
    };
  }
  trackByFn(item: UsuarioDTO): number {
    return item.idEspecialidad; // Usa una propiedad única del usuario si es posible
  }
  onEdit(index: number, field: string): void {
    const partida = this.list.at(index).value.codPartida;
    this.editStates[partida][field] = true;
  }
  isEdit(index: number, field: any): boolean {
    const user = this.list.at(index).value.codPartida;
    console.log(user);
    return this.editStates[user] ? this.editStates[user][field] : false;
  }

  handleRowUpdate(index: number): void {
    this.onRowUpdate.emit(index);
  }
}
