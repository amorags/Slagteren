import { Component } from '@angular/core';
import {State} from "../../state";
import {FormBuilder, Validators} from "@angular/forms";
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {ToastController} from "@ionic/angular";
import {Product, ResponseDto} from "../../models";
import {environment} from "../../environments/environment.prod";
import {firstValueFrom} from "rxjs";

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.scss'],
})
export class AddProductComponent {

  createNewProductForm = this.fb.group({
    productNumber: [0, Validators.required],
    productName: ['',[Validators.minLength(4), Validators.required]],
    pricePrKilo: [0, Validators.required],
    productType: [0, Validators.required],
    countryOfBirth: ['', Validators.required],
    productionCountry: ['', Validators.required],
    description: ['',[Validators.maxLength(500), Validators.required]],
    imgUrl: ['', Validators.maxLength(500)],
    minExpDate: [0, Validators.required]
  })

  constructor(public fb: FormBuilder, public http: HttpClient,
              public state: State, public toastController: ToastController) {
  }

  selectedProductTypeId: number = 0;

  // Event handler for ionChange
  onProductTypeChange(event: CustomEvent) {
    const selectedValue = event.detail.value;
    this.selectedProductTypeId = parseInt(selectedValue, 10);
  }


  async submit() {
    try {
      const observable = this.http.post<ResponseDto<Product>>(environment.baseUrl + '/api/product', this.createNewProductForm.getRawValue())

      const response = await firstValueFrom(observable);
      this.state.products.push(response.responseData!);

      const toast = await this.toastController.create({
        message: 'Produktet er blevet succesfuldt tilført',
        duration: 1233,
        color: "success"
      })
      toast.present();
    } catch (e) {
      console.log(e);
      if (e instanceof HttpErrorResponse) {
        const toast = await this.toastController.create({
          message: e.error.messageToClient,
          duration: 1233,
          color: "danger"
        });
        toast.present();
      }
    }
  }
}

