import { Component, OnInit } from '@angular/core';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model : any ={};

  constructor(public authService: AuthService , private alertify: AlertifyService) {  }

  ngOnInit() {
    
  }

  login()
  {
    this.authService.login(this.model).subscribe(
      next => { this.alertify.success('تم الدخول بنجاح'); },
      error => { this.alertify.error('فشل الدخول ') }
      
    )
  }


  loggedIn() {
    

    return this.authService.loggedIn();
     
  }

  

  loggedOut() {
    localStorage.removeItem('token');
    this.alertify.message('تم الخروج');
    this.loggedIn();
  }

}
