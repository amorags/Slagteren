import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import {HomeComponent} from "./home/home.component";
import {IonicModule} from "@ionic/angular";
import {HttpClientModule} from "@angular/common/http";
import {ListUserComponent} from "./list-customer/list-user.component";
import {ErrorComponent} from "./error/error.component";
import {LoginComponent} from "./login/login.component";
import {SignupComponent} from "./signup/signup.component";
import {AddProductComponent} from "./add-product/add-product.component";

const routes: Routes = [
  {
    path: 'home', component: HomeComponent
  },
  {
    path: 'list-customer', component: ListUserComponent
  },
  {
    path: 'login', component: LoginComponent
  },
  {
    path: 'signup', component: SignupComponent
  },
  {
    path: 'add-product', component: AddProductComponent
  },
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full'
  },
  {
    path: '**', component: ErrorComponent
  }

];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules }),
    HttpClientModule,
    IonicModule
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
