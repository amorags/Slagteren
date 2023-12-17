import { Component, OnInit, inject } from '@angular/core';
import {State} from "../../state";
import { ActivatedRoute } from "@angular/router";
import { ProductService } from "../product.service";
import {Product} from "../../models";

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.scss'],
})
export class ProductDetailComponent  implements OnInit {
  route: ActivatedRoute = inject(ActivatedRoute);
  productService = inject(ProductService)
  product: Product | undefined;

  constructor(public state: State) {
    const productId = Number(this.route.snapshot.params['id']);
    this.product = this.productService.GetProductById(productId);
  }


  ngOnInit() {}

}
