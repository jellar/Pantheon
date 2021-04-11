import {Inject, Injectable} from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import {Transaction} from "../account/account.model";

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getAccountDetails(): Observable<any> {
    return this.http.get(this.baseUrl + 'api/home', httpOptions);
  }

  getPagedTransactions(accountId: string, page: number, size:number): Observable<any> {
    return this.http.get(`${this.baseUrl}api/transaction/getpaged?accountId=${accountId}&page=${page}&size=${size}`, httpOptions);
  }

  getTransactionsToExport(accountId: string): Observable<any> {
    return this.http.get(`${this.baseUrl}api/transaction/export?accountId=${accountId}`, { responseType: 'blob' as 'json' });
  }

  postTransaction(transaction: Transaction): Observable<any> {
    return this.http.post(`${this.baseUrl}api/transaction`, transaction, httpOptions);
  }
}
