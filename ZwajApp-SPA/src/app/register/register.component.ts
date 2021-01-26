import { Component, Input, OnInit, Output , EventEmitter } from '@angular/core';
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
  constructor(private authService:AuthService) { }

  ngOnInit() {
  }

  register() {
    this.authService.Register(this.model).subscribe(
    ()=> {  console.log("تم التسجيل بنجاح")},
    error => { console.log(error)}
    
    )
     
  }
  cancel() {
    console.log('ليس الأن')
    this.cancelRegister.emit(false);
  }



}
