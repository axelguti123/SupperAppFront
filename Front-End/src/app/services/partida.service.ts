import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environment/environment';
import { Observable } from 'rxjs';
import { PartidaDTO } from '../dto/partidaDTO';
@Injectable({
  providedIn: 'root',
})
export class PartidaService {
  constructor(private http:HttpClient) {}
  private apiURL=environment.apiURL+'partida';

  public obtenerTodos(): Observable<{data:PartidaDTO[],message:string,status:string}> {
    return this.http.get<{data:PartidaDTO[],message:string,status:string}>(this.apiURL);
  }
  public crear(especialidad:PartidaDTO) {
    return this.http.post(this.apiURL,especialidad);
  }
}
