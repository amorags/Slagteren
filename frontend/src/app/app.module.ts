import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouteReuseStrategy } from '@angular/router';

import { IonicModule, IonicRouteStrategy } from '@ionic/angular';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {NavBarComponent} from "./nav-bar/nav-bar.component";
import {HomeComponent} from "./home/home.component";
import {ListUserComponent} from "./list-customer/list-user.component";
import {ErrorComponent} from "./error/error.component";
import {LoginComponent} from "./login/login.component";
import {SignupComponent} from "./signup/signup.component";
import {AddProductComponent} from "./add-product/add-product.component";
import { HttpClientModule } from '@angular/common/http';
import {CheckOutComponent} from "./check-out/check-out.component";
import {ProductDetailComponent} from "./product-detail/product-detail.component";

@NgModule({


  declarations: [AppComponent, NavBarComponent, HomeComponent, ListUserComponent, ErrorComponent, LoginComponent, SignupComponent, AddProductComponent, CheckOutComponent, ProductDetailComponent],

  imports: [BrowserModule, HttpClientModule, IonicModule.forRoot({mode: "ios"}), AppRoutingModule, FormsModule, ReactiveFormsModule],
  providers: [{ provide: RouteReuseStrategy, useClass: IonicRouteStrategy }],
  bootstrap: [AppComponent],
})
export class AppModule {}
