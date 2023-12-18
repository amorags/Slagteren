import { Component, OnInit, inject } from '@angular/core';
import {State} from "../../state";
import { ActivatedRoute } from "@angular/router";
import { ProductService } from "../product.service";
import {CartItem, Product} from "../../models";
import { FormControl, Validators} from "@angular/forms";
import {ToastController} from "@ionic/angular";

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.scss'],
})
export class ProductDetailComponent  implements OnInit {
  route: ActivatedRoute = inject(ActivatedRoute);
  productService = inject(ProductService)
  product: Product | undefined;

  amount = new FormControl(0, [Validators.required])


  constructor(private toastController: ToastController, public state: State) {
    const productId = Number(this.route.snapshot.params['id']);
    this.product = this.productService.GetProductById(productId);
  }


  ngOnInit() {
  }

  async presentToast(message: string, color: string) {
    const toast = await this.toastController.create({
      message: message,
      duration: 2000,
      color: color
    });
    toast.present();
  }

  onAddToCart(product: Product, amount: FormControl<number | null>) {
    try {
      const quantity = amount.value || 1; // Default to 1 if amount is null

      // Check if the product is already in the cart
      const existingCartItem: CartItem | undefined = this.state.cartItems.find(item => item.productId === product.productId);

      if (existingCartItem) {
        // If the product is already in the cart, update the quantity
        // @ts-ignore
        existingCartItem.quantity += quantity;
      } else {
        // If the product is not in the cart, add a new cart item
        this.state.cartItems.push({
          productId: product.productId,
          productName: product.productName,
          productImgUrl: product.imgUrl,
          pricePrKilo: product.pricePrKilo,
          quantity: quantity
        });
      }

      // Show success toast
      this.presentToast('Item added to cart', 'success');
    } catch (e) {
      console.error(e);

      // Show error toast
      this.presentToast('An error occurred', 'danger');
    }
  }
}
