import {
  AfterViewInit,
  ChangeDetectionStrategy,
  Component,
  ElementRef,
  EventEmitter,
  Input,
  OnDestroy,
  Output,
  ViewChild,
} from '@angular/core';
import { UsuarioDTO } from '../../../dto/usuarioDTO';
import { Config } from 'datatables.net';
import { Subject } from 'rxjs';
import { FormArray, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-tabla-datos',
  templateUrl: './tabla-datos.component.html',
  styleUrl: './tabla-datos.component.scss',
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class TablaDatosComponent implements OnDestroy, AfterViewInit {
  constructor(){

  }
  ngAfterViewInit(): void {
    
  }

  ngOnDestroy(): void {
  }

  onDelete(event:{index:number,idPartida:number}):void{
    console.log(event.index,event.idPartida)
    this.onRowDelete.emit({index:event.index,idPartida:event.idPartida});
    console.log('Registro Eliminado');
  }
  
  @Input() forms: FormGroup;  
  @Input() editStates = {};
  @Output() onRowUpdate = new EventEmitter<number>();
  @Output() onRowDelete = new EventEmitter<{index: number, idPartida: any}>();
  @Input() columns: string[];
  @Input() dtOptions: Config = {};
  @Input() dtTrigger = new Subject<Config>();
  get list(): FormArray {
    return this.forms.get('list') as FormArray;
  }
  trackByFn(index: number, item: FormGroup): number {
    console.log(index)
    return item.value.idPartida; // Usa una propiedad única del usuario si es posible
  }
  onEdit(index: number, field: string): void {
    const partida = this.list.at(index).value.idPartida;
    this.editStates[partida][field] = true;
  }
  isEdit(index: number, field: any): boolean {
    const user = this.list.at(index).value.idPartida;
    console.log(user);
    return this.editStates[user] ? this.editStates[user][field] : false;
  }
  handleRowUpdate(index: number): void {
    this.onRowUpdate.emit(index);
  }
}
