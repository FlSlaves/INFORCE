import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit{
  regForm!: FormGroup;
  constructor(private fb: FormBuilder, private auth: AuthService, private router: Router){}

  ngOnInit(): void {
    this.regForm = this.fb.group({
      email: ['', Validators.required],
      username: ['', Validators.required],
      password: ['', Validators.required],
    })
  }
  onReg() {

      this.auth.register(this.regForm.value)
        .subscribe({
          next: (res) => {
            alert(res.message);
            this.regForm.reset();
            this.router.navigate(['login']);
          },
          error: (err) => {
            alert(err?.error.message)
          }
        })
    
  }
}
