import { Injectable } from '@angular/core';
import * as XLSX from 'xlsx';

@Injectable({
  providedIn: 'root',
})
export class ExcelToJson {
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
    };
    fileReader.readAsArrayBuffer(file);
    console.log(file);
  }
}
