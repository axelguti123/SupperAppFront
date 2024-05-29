import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environment/environment';
import { Observable } from 'rxjs';
import { UsuarioDTO } from '../dto/usuarioDTO';
import { MostrarUsuarioDTO } from '../dto/MostrarUsuarioDTO';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {

  constructor(private http:HttpClient) { }
  private apiURL=environment.apiURL+"usuario";
  public obtenerTodos():Observable<MostrarUsuarioDTO>{
    return this.http.get<MostrarUsuarioDTO>(this.apiURL)
  }
}
