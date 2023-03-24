
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { User } from 'src/app/models/user';
import { AuthService } from 'src/app/services/auth.service';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  private _isLogged$ = new BehaviorSubject<boolean>(false);
  loginForm!: FormGroup;
  user!: User;


  constructor(private fb:FormBuilder, private auth: AuthService, private router: Router) { 

  }
 
  ngOnInit(): void {
    this.loginForm = this.fb.group({
      email: ['', Validators.required],
      password: ['', Validators.required],
    })
  }
  onLogin(){     
    console.log(this.loginForm.value);
      this.auth.login(this.loginForm.value)
      .subscribe({
        next:(res)=>{
          alert(res.message);
          this.loginForm.reset();
          this._isLogged$.next(true);
          localStorage.setItem('auth', res.token);
          this.auth.refresh();
          this.router.navigate(['urltable']);      
        },
        error:(err)=>{
          alert(err?.error.message)
        }
      })  
  }
}
