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
import {WebShopComponent} from "./web-shop/web-shop.component";

const routes: Routes = [
  {
    path: 'home', component: HomeComponent,
    title: 'Hjemmeside'
  },
  {
    path: 'details/:id', component: ProductDetailComponent,
    title: 'Detaljer'
  },
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full'
  },
  {
    path: '**', component: ErrorComponent
  },
  {
    path: 'list-customer', component: ListUserComponent
  },
  {
    path: 'login', component: LoginComponent,
    title: 'Log ind'
  },
  {
    path: 'signup', component: SignupComponent,
    title: 'Tilmeld dig'
  },
  {
    path: 'add-product', component: AddProductComponent,
    title: 'Tilføj produkt'
  },
  {
    path: 'check-out', component: CheckOutComponent,
    title: 'Check ud'
  },
  {
    path: 'web-shop', component: WebShopComponent,
  },


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
