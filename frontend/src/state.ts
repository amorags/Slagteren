import {Injectable} from "@angular/core";
import {Product} from "./models";

@Injectable({
  providedIn: 'root'
})
export class State {
  products: Product[] = [
    {
      product_id: 1,
      product_number: 800021,
      product_name: 'hamburgerryg',
      product_img: './../assets/hamburgerryg-kogetid.jpg',
      price_per_kilo: 59.95,
      description: 'Det er en hamburgerryg',
      Expiration_date: 21,
    },
    {
      product_id: 2,
      product_number: 800021,
      product_name: 'hamburgerryg',
      product_img: './../assets/hamburgerryg-kogetid.jpg',
      price_per_kilo: 59.95,
      description: 'Det er en hamburgerryg',
      Expiration_date: 21,
    },
    {
      product_id: 3,
      product_number: 800021,
      product_name: 'hamburgerryg',
      product_img: './../assets/hamburgerryg-kogetid.jpg',
      price_per_kilo: 59.95,
      description: 'Det er en hamburgerryg',
      Expiration_date: 21,
    },
    {
      product_id: 4,
      product_number: 800021,
      product_name: 'hamburgerryg',
      product_img: './../assets/hamburgerryg-kogetid.jpg',
      price_per_kilo: 59.95,
      description: 'Det er en hamburgerryg',
      Expiration_date: 21,
    },

  ];
}
