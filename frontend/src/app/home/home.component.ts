import {Component, inject, OnInit} from '@angular/core';

import {State} from "../../state";
import { ProductService } from "../product.service";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {

  productService = inject(ProductService)

  constructor(public Http: HttpClient, public state: State) {

  }



  ngOnInit(): void {
    this.productService.fetchProduct()
  }
}
