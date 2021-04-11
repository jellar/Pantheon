import {Inject, Injectable} from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  login(email: string, password: string): Observable<any> {
    return this.http.post(this.baseUrl + 'api/account/authenticate', {
      email,
      password
    }, httpOptions);
  }
}
