import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouteReuseStrategy } from '@angular/router';

import { IonicModule, IonicRouteStrategy } from '@ionic/angular';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import {FormsModule} from "@angular/forms";
import {NavBarComponent} from "./nav-bar/nav-bar.component";
import {HomeComponent} from "./home/home.component";
import {ListCustomerComponent} from "./list-customer/list-customer.component";
import {ErrorComponent} from "./error/error.component";

@NgModule({
  declarations: [AppComponent, NavBarComponent, HomeComponent, ListCustomerComponent, ErrorComponent],
  imports: [BrowserModule, IonicModule.forRoot({mode: "ios"}), AppRoutingModule, FormsModule],
  providers: [{ provide: RouteReuseStrategy, useClass: IonicRouteStrategy }],
  bootstrap: [AppComponent],
})
export class AppModule {}
