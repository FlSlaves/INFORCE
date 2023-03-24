import { Component, OnInit } from '@angular/core';
import { AuthService } from './services/auth.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Inforce';
  isLog: any;
  constructor(private auth: AuthService){}
  ngOnInit(): void {
      if(this.auth.isLogged())
        this.isLog = true;
      else
        this.isLog = false;
  }
  logOut(){
    this.auth.singOut();
    this.auth.refresh()
  }

}
