import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import {gsap} from 'gsap'
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit{

  loginObj: any = {
    userName: '',
    password: ''
  }
  constructor(private router: Router) {
    
  }
  ngOnInit(): void {
    gsap.from('.signin',{
      x:-600,
      fill:'blue'
    })
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
