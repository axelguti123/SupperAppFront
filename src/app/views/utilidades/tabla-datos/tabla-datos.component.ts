import {
  AfterViewInit,
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
})
export class TablaDatosComponent implements OnDestroy, AfterViewInit {
  constructor(){

  }
  ngAfterViewInit(): void {
    
  }
  ngOnDestroy(): void {
  }

  onDelete(event:{index:number,codPartida:any}):void{
    this.list.removeAt(event.index);
    console.log(event.index,event.codPartida)
    console.log('Registro Eliminado');
  }
  
  @Input() forms: FormGroup;  
  @Input() editStates = {};
  @Output() onRowUpdate = new EventEmitter<number>();
  @Input() columns: string[];
  @Input() dtOptions: Config = {};
  @Input() dtTrigger = new Subject<Config>();
  get list(): FormArray {
    return this.forms.get('list') as FormArray;
  }
  trackByFn(index: number, item: FormGroup): number {
    console.log(index)
    return item.value.codPartida; // Usa una propiedad única del usuario si es posible
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
