import { HttpClient } from '@angular/common/http';
import { Component, OnDestroy, OnInit, Output } from '@angular/core';
import { Config } from 'datatables.net';
import { Subject } from 'rxjs';
import { UsuarioService } from '../../../services/usuario.service';
import { UsuarioDTO } from '../../../dto/usuarioDTO';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrl: './user.component.scss'
})
export class UserComponent implements OnInit,OnDestroy {
  @Output() userArray: any[];
  dtOptions: Config={};
  dtTrigger=new Subject();
  constructor(private usuarioService: UsuarioService
  ) {}
  ngOnDestroy(): void {
    
  }
  ngOnInit(): void {
    this.dtOptions = {
      pagingType:'full-numbers'
    }
    this.loadAllUser();
  }

  loadAllUser() {
    this.usuarioService.obtenerTodos().subscribe({
      next: (usuario:UsuarioDTO[]) => {
        this.userArray = usuario;
        console.log(this.userArray);
      },
      error: (error) => console.error(error),
    });
  }
  originalValue: any;
  onEdit(item: any) {
    this.originalValue={...item};
    item.isEdit = true;
  }
  onUpdate(data: any): void {
      console.log(data);
  }

  onRowUpdate(user:any):void{
    
    if (this.originalValue && JSON.stringify(user) !== JSON.stringify(this.originalValue)) {
      // El valor ha cambiado    
      this.onUpdate(user);
    }
    user.isEdit = !user.isEdit;
  }
  validateField(item: any) {
    if (item !== '') {
      return false;
    } else {
      return true;
    }
  }
  validateUserName(userName: string) {
    if (userName === '') {
      return 'Required';
    } else {
      if (userName.length >= 3) {
        return '';
      } else {
        return 'min 3 char';
      }
    }
  }
  validateForm(obj: any) {
    if (obj.name != '' && obj.username != '' && obj.phone != '') {
      return false;
    } else {
      return true;
    }
  }
  onCancel(item: any) {
    item.isEdit = false;
  }
  trackByFn(index: number, item: any) {
    return item.id; // Usa una propiedad única del usuario si es posible
  }
  selectedUser: any;

  selectRow(user: any) {
    this.selectedUser = user;
  }
}
