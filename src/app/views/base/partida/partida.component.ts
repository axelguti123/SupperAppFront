import {
  AfterViewInit,
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  ElementRef,
  OnDestroy,
  OnInit,
  ViewChild,
} from '@angular/core';
import { Config } from 'datatables.net';
import { Subject, Subscription, takeUntil, tap } from 'rxjs';
import { PartidaDTO } from '../../../dto/partidaDTO';
import { PartidaService } from '../../../services/partida.service';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';
import { spanishTranslation } from '../../traduccionDatatables';

@Component({
  selector: 'app-partida',
  templateUrl: './partida.component.html',
  styleUrl: './partida.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class PartidaComponent implements OnInit, OnDestroy,AfterViewInit {
  dtOptions: Config = {};
  private unsubscribe$ = new Subject<void>();
  partidaForm: FormGroup;
  editStates: { [key: string]: any } = {};
  columns: string[]
  dtTrigger=new Subject<Config>();
  @ViewChild('datatable',{static:true}) table:ElementRef

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
    
    
  }
  ngOnDestroy(): void {
    this.dtTrigger.unsubscribe();
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
    this.unSuscribeFromData();
  }
  private unSuscribeFromData(): void{
    if(this.dataSubscription && this.dataSubscription.closed){
      this.dataSubscription.unsubscribe();
    }
  }
  private initializeDataTableOptions(): void {
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 10,
      columns:[
        {title:'Nro'},
        {title:'Item'},
        {title:'Partida'},
        {title:'Und'},
        {title:'Total'},
        {title:'Delete',orderable:false,searchable:false}
      ]
    };
  }
  ngOnInit(): void {
    this.subsCribeData();
    this.initializeDataTableOptions();
    
  }
  private dataSubscription: Subscription;
  private subsCribeData(): void {
    if (!this.dataSubscription || this.dataSubscription.closed) {
      const startTime = performance.now();
      this.dataSubscription=this.partidaService
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
            this.InitializeForm(partida.data);
            this.dtTrigger.next(this.dtOptions);
            this.ref.markForCheck();
          },
          error: (error) => console.error(error),
        });
    }
  }
  private InitializeForm(partida:PartidaDTO[]):void{
    const partidaArray = partida.map((partida) =>
      this.createPartida(partida)
    );
    this.partidaForm.setControl(
      'list',
      this.fb.array(partidaArray)
    );
  }
  createPartida(data: PartidaDTO): FormGroup {
    const partidaForm = this.fb.group({
      codPartida: [data.codPartida],
      partida: [data.partida],
      und: [data.und],
      total: [data.total]
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

  onUpdate(data: PartidaDTO): void {
    this.partidaService.update(data).subscribe();

  }
  onRowUpdate(index: number): void {
    const partida = this.list.at(index).value.codPartida;
    const data = this.list.at(index).value;
    this.onUpdate(data);
    Object.keys(this.editStates[partida]).forEach((field) => {
      this.editStates[partida][field] = false;
    });
    this.ref.markForCheck();

  }

}
