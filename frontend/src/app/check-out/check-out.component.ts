import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormControl, Validators} from '@angular/forms';
import {State} from "../../state";
import {ToastController} from "@ionic/angular";
import {Router} from "@angular/router";

@Component({
  selector: 'app-check-out',
  templateUrl: './check-out.component.html',
  styleUrls: ['./check-out.component.scss'],
})
export class CheckOutComponent implements OnInit {
  createNewOrderForm = this.fb.group({
    productId: [0, Validators.required],
  });

  price = new FormControl(0, [Validators.required]);
  quantity = new FormControl(0, [Validators.required]);
  total: number | null = null;

  constructor(
    public fb: FormBuilder,
    public state: State,
    private toastController: ToastController,
    private router: Router
  ) {}

  async presentToast(message: string, color: string) {
    const toast = await this.toastController.create({
      message: message,
      duration: 2000,
      color: color,
    });
    await toast.present();
  }

  clearCart() {
    // Clear all items from the cart
    this.state.cartItems = [];
  }

  async confirmOrder() {
    try {
      // Your logic to confirm the order goes here

      // Show a success toast
      await this.presentToast('Order confirmed!', 'success');

      // Clear the cart
      this.clearCart();

      // Navigate to the homepage
      await this.router.navigate(['/home']);
    } catch (error) {
      console.error(error);

      // Show an error toast
      await this.presentToast('An error occurred. Please try again.', 'danger');
    }
  }

  async calculateTotal() {
    const product = this.state.products.find((p) => p.productId === this.createNewOrderForm.value.productId);

    // Check if product, this.price, and this.quantity are not null
    if (product && this.price.value !== null && this.quantity.value !== null) {
      this.total = this.price.value * this.quantity.value;
    } else {
      // Handle the case where the product or form controls are null
      this.total = null;
    }
  }



  removeItem(productId: number | undefined) {
    if (productId !== undefined) {
      // Find the index of the item with the given productId
      const index = this.state.cartItems.findIndex(
        (item) => item.productId === productId
      );

      // If the item is found, remove it from the array
      if (index !== -1) {
        this.state.cartItems.splice(index, 1);
      }
    }
  }
  isCartEmpty(): boolean {
    return this.state.cartItems.length === 0;
  }

  ngOnInit() {
    this.subscribeToFormChanges();
  }

  subscribeToFormChanges() {
    this.price.valueChanges.subscribe(() => {
      this.calculateTotal();
    });

    this.quantity.valueChanges.subscribe(() => {
      this.calculateTotal();
    });
  }
}
