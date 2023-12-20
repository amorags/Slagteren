import {Injectable, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {State} from "../state";
import {firstValueFrom} from "rxjs";
import {Product, ResponseDto} from "../models";
import {environment} from "../environments/environment.prod";

@Injectable({
  providedIn: 'root'
})
export class ProductService {


  constructor(public Http: HttpClient, public state: State) { }

  async fetchProduct() {
    const result = await firstValueFrom(this.Http.get<ResponseDto<Product[]>>(environment.baseUrl + '/api/products'))
    this.state.products = result.responseData!;
  }


  GetProductById(productId: number): Product | undefined {
    return this.state.products.find(product => product.productId === productId);
  }


}
