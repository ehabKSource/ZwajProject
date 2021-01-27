import { Component, Input, OnInit, Output , EventEmitter } from '@angular/core';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  @Input() ValuseForRegister : any;
  @Output() cancelRegister = new EventEmitter()  ;


  model:any ={};
  constructor(private authService:AuthService , private alertify: AlertifyService) { }

  ngOnInit() {
  }

  register() {
    this.authService.Register(this.model).subscribe(
    ()=> {  this.alertify.success("تم التسجيل بنجاح")},
    error => { this.alertify.error(error)}
    
    )
     
  }
  cancel() {
    console.log('ليس الأن')
    this.cancelRegister.emit(false);
  }



}
