import {
  AfterViewInit,
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  OnDestroy,
  OnInit,
} from '@angular/core';
import { Config } from 'datatables.net';
import { Subject, Subscription, takeUntil, tap } from 'rxjs';
import { UsuarioDTO } from '../../../dto/usuarioDTO';
import { PartidaDTO } from '../../../dto/partidaDTO';
import { PartidaService } from '../../../services/partida.service';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-partida',
  templateUrl: './partida.component.html',
  styleUrl: './partida.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class PartidaComponent implements OnInit, OnDestroy,AfterViewInit {
  dtOptions: Config = {};
  dtTrigger = new Subject<Config>();
  private unsubscribe$ = new Subject<void>();
  partidaForm: FormGroup;
  editStates: { [key: string]: any } = {};
  columns: string[]
  constructor(
    private partidaService: PartidaService,
    private fb: FormBuilder,
    private ref: ChangeDetectorRef
  ) {
    this.partidaForm = fb.group({
      list: this.fb.array([]),
    });
  }
  ngAfterViewInit(): void {
    this.dtTrigger.next(this.dtOptions);
  }
  ngOnDestroy(): void {
    this.dtTrigger.unsubscribe();
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }
  private initializeDataTableOptions(): void {
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 10,
    };
  }
  ngOnInit(): void {
    this.subsCribeData();
    this.initializeDataTableOptions();
    this.columns= ['Nro','Item','Partida','Und','Total'];
  }
  private dataSubscription: Subscription;
  private subsCribeData(): void {
    if (!this.dataSubscription || this.dataSubscription.closed) {
      const startTime = performance.now();
      this.partidaService
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
          next: (partida: {
            data: PartidaDTO[];
            message: string;
            status: string;
          }) => {
            const partidaArray = partida.data.map((partida) =>
              this.createPartida(partida)
            );
            this.partidaForm.setControl(
              'list',
              this.fb.array(partidaArray)
            );
            this.ref.markForCheck();
          },
          error: (error) => console.error(error),
        });
    }
  }
  createPartida(data: PartidaDTO): FormGroup {
    const partidaForm = this.fb.group({
      codPartida: [data.codPartida],
      partida: [data.partida],
      und: [data.und],
      total: [data.total],
    });
    this.editStates[data.codPartida] = {
      codPartida: false,
      partida: false,
      und: false,
      total: false,
    };
    return partidaForm;
  }
  get list() {
    return this.partidaForm.get('list') as FormArray;
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
  onUpdate(data: PartidaDTO): void {
    this.partidaService.update(data).subscribe();
  }
  onRowUpdate(index: number): void {
    const user = this.list.at(index).value.codPartida;
    const data = this.list.at(index).value;
    console.log('Datos actualizados:', data);
    this.onUpdate(data);
    Object.keys(this.editStates[user]).forEach((field) => {
      this.editStates[user][field] = false;
    });
  }
  trackByFn(index: number, item: UsuarioDTO) {
    return item.idEspecialidad; // Usa una propiedad única del usuario si es posible
  }
}
