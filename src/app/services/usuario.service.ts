import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environment/environment';
import { Observable } from 'rxjs';
import { UsuarioDTO } from '../dto/usuarioDTO';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {

  constructor(private http:HttpClient) { }
  private apiURL=environment.apiURL+"usuario";
  public obtenerTodos():Observable<{data:UsuarioDTO[],message:string,status:string}>{
    return this.http.get<{data:UsuarioDTO[],message:string,status:string}>(this.apiURL)
  }
}
