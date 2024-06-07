import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-seguimiento',
  templateUrl: './seguimiento.component.html',
  styleUrl: './seguimiento.component.scss',
})
export class SeguimientoComponent {
  userArray: any[] = [];
  constructor(private http: HttpClient) {}
  ngOnInit(): void {
    this.loadAllUser();
  }

  loadAllUser() {
    this.http
      .get('https://jsonplaceholder.typicode.com/users')
      .subscribe((res: any) => {
        this.userArray = res;
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
