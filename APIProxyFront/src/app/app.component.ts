import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'APIProxyFront';
  data: any = [];

  constructor(private http: HttpClient) {}

  loadData() {
    this.http.get<any>('https://localhost:44307/nbshare/').subscribe(result => {
      this.data = result;
    }, error => {
      console.log(error);
    });    
  }
}
