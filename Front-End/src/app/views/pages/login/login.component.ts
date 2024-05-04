import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {

  loginObj: any = {
    userName: '',
    password: ''
  }
  constructor(private router: Router) {
    
  }
  onLogin() {
    if(this.loginObj.userName == 'admin' &&
     this.loginObj.password == 'admin') {

      localStorage.setItem('loginUserName',
      this.loginObj.userName );
      this.router.navigateByUrl("dashboard")
      
    } else {
      alert("Wrong Details");
    }
  }

}
