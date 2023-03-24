import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AutguardGuard } from './assets/autguard.guard';
import { DetailviewComponent } from './components/detailview/detailview.component';
import { LoginComponent } from './components/login/login.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { URLtableComponent } from './components/urltable/urltable.component';


const routes: Routes = [
  {path: 'login', component: LoginComponent},
  {path: 'registration', component: RegistrationComponent },
  {path: 'urltable', component: URLtableComponent },
  {path: 'detailview', component: DetailviewComponent,
  
  }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
