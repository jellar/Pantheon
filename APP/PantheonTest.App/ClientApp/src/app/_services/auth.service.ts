import {Inject, Injectable} from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {BehaviorSubject, Observable} from 'rxjs';
import {Router} from "@angular/router";
import {TokenStorageService} from "./token-storage.service";

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private loggedIn: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

  get isLoggedIn() {
    return this.loggedIn.asObservable();
  }
  constructor(private http: HttpClient,
              @Inject('BASE_URL') private baseUrl: string,
              private router: Router,
              private tokenStorage: TokenStorageService,) { }

  login(email: string, password: string): any{
    this.http.post(this.baseUrl + 'api/account/authenticate', {
      email,
      password
    }, httpOptions).subscribe(data=> {
       this.tokenStorage.saveToken(data);
       this.tokenStorage.saveUser(data);
       this.loggedIn.next(true);
       this.router.navigate(['/']);
      // return true;
    }, error => console.error(error));
  }

  logout() {
    this.loggedIn.next(false);
    this.tokenStorage.signOut();
    this.router.navigate(['/login']);
  }

}
