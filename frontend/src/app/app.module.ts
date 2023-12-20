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
import { HttpClientModule, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import {CheckOutComponent} from "./check-out/check-out.component";
import {ProductDetailComponent} from "./product-detail/product-detail.component";
import {TokenServiceService} from "../../serviceAngular/token-service.service"
import { Observable } from 'rxjs';
import {WebShopComponent} from "./web-shop/web-shop.component";

@NgModule({


  declarations: [AppComponent,
    NavBarComponent,
    HomeComponent,
    ListUserComponent,
    ErrorComponent,
    LoginComponent,
    SignupComponent,
    AddProductComponent,
    CheckOutComponent,
    ProductDetailComponent,
    WebShopComponent],

  imports: [BrowserModule, HttpClientModule, IonicModule.forRoot({mode: "ios"}), AppRoutingModule, FormsModule, ReactiveFormsModule],
  providers: [{ provide: RouteReuseStrategy, useClass: IonicRouteStrategy }],
  bootstrap: [AppComponent],
})


export class AppModule {}

export class AuthHttpInterceptor implements HttpInterceptor {constructor(private readonly service: TokenServiceService) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = this.service.gettoken();
    if (token && this.sameOrigin(req)) {
      return next.handle(req.clone({
        headers: req.headers.set("Authorization", `Bearer ${token}`)
      }));
    }
    return next.handle(req);
  }

  private sameOrigin(req: HttpRequest<any>) {
    const isRelative = !req.url.startsWith("http://") || !req.url.startsWith("https://");
    return req.url.startsWith(location.origin) || isRelative;
  }
}
