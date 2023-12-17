import {Component, inject, OnInit} from '@angular/core';

import {State} from "../../state";
import {firstValueFrom} from "rxjs";
import {Product, ResponseDto} from "../../models";
import {environment} from "../../environments/environment.prod";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit{

  constructor(public Http: HttpClient, public state: State) {

  }

  async fetchProduct() {
    const result = await firstValueFrom(this.Http.get<ResponseDto<Product[]>>(environment.baseUrl + '/api/products'))
    this.state.products = result.responseData!;
  }

  ngOnInit(): void {
    this.fetchProduct()
  }
}
