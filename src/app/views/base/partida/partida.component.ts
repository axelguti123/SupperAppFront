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
import * as XLSX from 'xlsx';
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
            console.log(partida.data)
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
      idPartida:[data.idPartida],
      codPartida: [data.codPartida],
      partida: [data.partida],
      und: [data.und],
      total: [data.total]
    });
    this.editStates[data.idPartida] = {
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
    const partida = this.list.at(index).value.idPartida;
    const data = this.list.at(index).value;
    this.onUpdate(data);
    Object.keys(this.editStates[partida]).forEach((field) => {
      this.editStates[partida][field] = false;
    });
    this.ref.markForCheck();

  }
  onRowDelete(event:{index:number,idPartida:number}): void {
    
    this.partidaService.delete(event.idPartida).subscribe({
      next: (partida: {
        message: string;
        status: string;
      }) => {
        console.log('Registro Eliminado');
        this.validarMessagge(partida,event.index);
      },
      error: (error) => console.error(error),
    });
  }
  validarMessagge(partida:any,index:number):void{
    if(partida.status==="success"){
      this.list.removeAt(index);
      this.ref.markForCheck();
    }else if(partida.status==="error"){
      console.log(partida.message);
    }
  }
  data: any;
  onFileChangeEvent(evt:any){
    const target: DataTransfer=<DataTransfer>(evt.target);
    if(target.files.length!==1) throw new Error("Cannot use multiple files");
    const reader:FileReader=new FileReader();
    reader.onload=(e:any)=>{
      const bstr: string=e.target.result;
      const wb: XLSX.WorkBook=XLSX.read(bstr,{type:'binary'});
      const wsname: string=wb.SheetNames[0];
      const ws: XLSX.WorkSheet=wb.Sheets[wsname];
      const jsonData= XLSX.utils.sheet_to_json(ws,{header:1})
      this.data=this.buildHierarchy(jsonData);
      console.log(this.data);
    };
    reader.readAsArrayBuffer(target.files[0])
  }
  buildHierarchy(data:any[]):any[]{
    const hierarchy=[];
    const lookup={}
    data.forEach((row,index)=>{
      if (index === 0) return; // Skip header row
      const [id, description, unit, quantity] = row;
      const item = { id, description, unit, quantity, children: [] };

      lookup[id] = item;

      const parentId = id.slice(0, -3);
      if (lookup[parentId]) {
        lookup[parentId].children.push(item);
      } else {
        hierarchy.push(item);
      }
    });
    return hierarchy;
  }
}
