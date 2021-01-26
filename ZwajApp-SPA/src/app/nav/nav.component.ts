import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model : any ={};

  constructor(public authService: AuthService) { }

  ngOnInit() {
  }

  login()
  {
    this.authService.login(this.model).subscribe(
      next => { console.log('تم الدخول بنجاح'); },
      error => { console.log('فشل الدخول ') }
      
    )
  }


  loggedIn() {
     const token = localStorage.getItem('token');
     return !! token;
   
  }

  loggedOut() {
    localStorage.removeItem('token');
  }

}