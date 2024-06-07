import { Injectable } from '@angular/core';
import { especialidadDTO } from '../dto/especialidadDTO';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environment/environment';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root',
})
export class EspecialidadService {
  constructor(private http:HttpClient) {}
  private apiURL=environment.apiURL+'especialidad';

  public obtenerTodos(): Observable<especialidadDTO[]> {
    return this.http.get<especialidadDTO[]>(this.apiURL);
  }
  public crear(especialidad:especialidadDTO) {
    return this.http.post(this.apiURL,especialidad);
  }
}
