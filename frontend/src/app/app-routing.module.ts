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
import {CheckOutComponent} from "./check-out/check-out.component";
import {ProductDetailComponent} from "./product-detail/product-detail.component";

const routes: Routes = [
  {
    path: 'home', component: HomeComponent,
    title: 'Home Page'
  },
  {
    path: 'details/:id', component: ProductDetailComponent,
    title: 'Details'
  },
  {
    path: 'list-customer', component: ListUserComponent
  },
  {
    path: 'login', component: LoginComponent,
    title: 'Log in'
  },
  {
    path: 'signup', component: SignupComponent,
    title: 'Sign up'
  },
  {
    path: 'add-product', component: AddProductComponent,
    title: 'Add product'
  },
  {
    path: 'check-out', component: CheckOutComponent,
    title: 'Check out'
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
