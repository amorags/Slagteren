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

  constructor( public state: State) { }

  GetProductById(productId: number): Product | undefined {
    return this.state.products.find(product => product.productId === productId);
  }
}
