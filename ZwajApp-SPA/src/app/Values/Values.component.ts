import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-Values',
  templateUrl: './Values.component.html',
  styleUrls: ['./Values.component.css']
})
export class ValuesComponent implements OnInit {
 values:any ;
  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getValues();
  }

  getValues(){

this.http.get("http://localhost:5000/Values").subscribe(
  Response=>{this.values=Response;  console.log(this.values) } ,

  error=>{ console.log(error)}

  )


  }

}
