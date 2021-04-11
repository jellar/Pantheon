import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import {AppRoutingModule} from "./app-routing.module";
import {authInterceptorProviders} from "./_helpers/auth.interceptor";
import {LoginComponent} from "./login/login.component";
import {AccountComponent} from "./account/account.component";
import {NgxPaginationModule} from "ngx-pagination";
import {TransactionComponent} from "./transaction/transaction.component";
import {RxReactiveFormsModule} from "@rxweb/reactive-form-validators";


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    LoginComponent,
    AccountComponent,
    TransactionComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    NgxPaginationModule,
    RxReactiveFormsModule
  ],
  providers: [authInterceptorProviders],
  bootstrap: [AppComponent]
})
export class AppModule { }
