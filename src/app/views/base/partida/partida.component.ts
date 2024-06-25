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
export class PartidaComponent implements OnInit, OnDestroy, AfterViewInit {
  dtOptions: Config = {};
  private unsubscribe$ = new Subject<void>();
  partidaForm: FormGroup;
  editStates: { [key: string]: any } = {};
  columns: string[];
  dtTrigger = new Subject<Config>();
  @ViewChild('datatable', { static: true }) table: ElementRef;

  constructor(
    private partidaService: PartidaService,
    private fb: FormBuilder,
    private ref: ChangeDetectorRef
  ) {
    this.partidaForm = fb.group({
      list: this.fb.array([]),
    });
  }
  ngAfterViewInit(): void {}
  ngOnDestroy(): void {
    this.dtTrigger.unsubscribe();
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
    this.unSuscribeFromData();
  }
  private unSuscribeFromData(): void {
    if (this.dataSubscription && this.dataSubscription.closed) {
      this.dataSubscription.unsubscribe();
    }
  }
  private initializeDataTableOptions(): void {
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 10,
      columns: [
        { title: 'Nro' },
        { title: 'Item' },
        { title: 'Partida' },
        { title: 'Und' },
        { title: 'Total' },
        { title: 'Delete', orderable: false, searchable: false },
      ],
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
      this.dataSubscription = this.partidaService
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
            console.log(partida.data);
            this.dtTrigger.next(this.dtOptions);
            this.ref.markForCheck();
          },
          error: (error) => console.error(error),
        });
    }
  }
  private InitializeForm(partida: PartidaDTO[]): void {
    const partidaArray = partida.map((partida) => this.createPartida(partida));
    this.partidaForm.setControl('list', this.fb.array(partidaArray));
  }
  createPartida(data: PartidaDTO): FormGroup {
    const partidaForm = this.fb.group({
      idPartida: [data.idPartida],
      codPartida: [data.codPartida],
      partida: [data.partida],
      und: [data.und],
      total: [data.total],
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
  onRowDelete(event: { index: number; idPartida: number }): void {
    this.partidaService.delete(event.idPartida).subscribe({
      next: (partida: { message: string; status: string }) => {
        console.log('Registro Eliminado');
        this.validarMessagge(partida, event.index);
      },
      error: (error) => console.error(error),
    });
  }
  validarMessagge(partida: any, index: number): void {
    if (partida.status === 'success') {
      this.list.removeAt(index);
      this.ref.markForCheck();
    } else if (partida.status === 'error') {
      console.log(partida.message);
    }
  }
  data: any;
  onFileChangeEvent(evt: any) {
    const file = evt.target.files[0];
    this.readFile(file);
  }
  readFile(file: File): void {
    const fileReader = new FileReader();
    fileReader.onload = () => {
      const arrayBuffer = fileReader.result as ArrayBuffer;
      const data = new Uint8Array(arrayBuffer);
      const arr = Array.from(data, (byte) => String.fromCharCode(byte)).join(
        ''
      );
      const workbook = XLSX.read(arr, { type: 'binary' });
      const sheetName = workbook.SheetNames[0];
      const workSheet = workbook.Sheets[sheetName];
      const jsonData = XLSX.utils.sheet_to_json(workSheet, { raw: true });
      console.log(jsonData);
      const completo=this.addIdPadre(jsonData);
      console.log(completo);
    };
    fileReader.readAsArrayBuffer(file);
    console.log(file);
  }
  addIdPadre(items: any[]): any[] {
    let idPadre: string | null = null;
    return items.map(item => {
      if (!item.ITEM.includes('.')) {
        idPadre = item.ITEM;
        return item;
      } else {
        return { ...item, IDPADRE: idPadre };
      }
    });
  } 
}
