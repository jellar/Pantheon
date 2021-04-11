import {NgModule} from "@angular/core";
import {RouterModule, Routes} from "@angular/router";
import {AccountComponent} from "./account/account.component";
import {LoginComponent} from "./login/login.component";
import {authInterceptorProviders} from "./_helpers/auth.interceptor";
import {AuthGuard} from "./_helpers/authguard";
import {TransactionComponent} from "./transaction/transaction.component";


const routes: Routes = [
  {path: 'account', component: AccountComponent, canActivate: [AuthGuard]},
  {path: 'account/:id', component: TransactionComponent,  canActivate:[AuthGuard]},
  {path: 'login', component: LoginComponent},
  { path: '', redirectTo: 'account', pathMatch: 'full' }
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports:[RouterModule],
  providers: [authInterceptorProviders]
})

export class AppRoutingModule {}
