import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { TokenStorageService } from '../_services/token-storage.service';
import {Router} from "@angular/router";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  form: any = {
    email: 'user@test.com',
    password: 'Plural&01?'
  };
  isLoggedIn = false;
  isLoginFailed = false;
  errorMessage = '';

  constructor(private authService: AuthService, private tokenStorage: TokenStorageService) { }

  ngOnInit(): void {
    if (this.tokenStorage.getToken()) {
      //this.isLoggedIn = true;
    }
  }

  onSubmit(): void {
    const { email, password } = this.form;

   const result =  this.authService.login(email, password);
   if(!result){
     //this.isLoginFailed = true;
   }
  }

  reloadPage(): void {
    window.location.reload();
  }
}
