import { BrowserModule } from '@angular/platform-browser';
import { NgModule, OnInit } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from "@angular/forms";

import { AppComponent } from './app.component';
import { ValuesComponent } from './Values/Values.component';
import { NavComponent } from './nav/nav.component';
import { AuthService} from './_services/auth.service';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { ErrorInterceptorProvidor } from './_services/error.interceptor';
import { AlertifyService } from './_services/alertify.service';


@NgModule({
  declarations: [				
    AppComponent,
      ValuesComponent,
      NavComponent,
      HomeComponent,
      RegisterComponent
   ],
  imports: [
    BrowserModule , HttpClientModule , FormsModule
  ],
  providers: [AuthService ,ErrorInterceptorProvidor , AlertifyService ],
  bootstrap: [AppComponent]
})
export class AppModule  {

  

 }
