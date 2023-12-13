import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import {HomeComponent} from "./home/home.component";
import {IonicModule} from "@ionic/angular";
import {HttpClientModule} from "@angular/common/http";
import {ListCustomerComponent} from "./list-customer/list-customer.component";
import {ErrorComponent} from "./error/error.component";

const routes: Routes = [
  {
    path: 'home', component: HomeComponent
  },
  {
    path: 'list-customer', component: ListCustomerComponent
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
