import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { especialidadDTO } from '../../../dto/especialidadDTO';

@Component({
  selector: 'app-combobox',
  templateUrl: './combobox.component.html',
  styleUrl: './combobox.component.scss'
})
export class ComboboxComponent implements OnChanges{
  @Input() especialidades:especialidadDTO[];
  @Input() idEspecialidad:number;
  ngOnChanges(changes: SimpleChanges): void {
    if (changes.idEspecialidad) {
      console.log('idEspecialidad changed:', changes.idEspecialidad.currentValue);
    }
    if (changes.especialidades) {
      console.log('especialidad changed:', changes.especialidad.currentValue);
    }
  }


}
