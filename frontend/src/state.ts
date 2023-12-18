import {Injectable} from "@angular/core";
import {User, Product, ProductType, CheckOutItem} from "./models";

@Injectable({
  providedIn: 'root'
})
export class State {
  products: Product[] = [];
  users: User[] = [];
  checkOutItems: CheckOutItem[] = [
    {
      productId: 1,
      productName: 'BBQ kylling',
      productImgUrl: 'https://www.slagtertheilgaard.dk/assets/Uploads/a085da6296/IMG_2445__FocusFillWzU0MCwzNTAsInkiLDI3XQ.jpeg',
      pricePrKilo: 99.95
    }
  ];
  productTypes: ProductType[] = [
    {
    typeId: 1,
    typeName: 'Oksekød'
    },
    {
      typeId: 2,
      typeName: 'Svinekød'
    },
    {
      typeId: 3,
      typeName: 'Fjerkræ'
    },
    {
      typeId: 4,
      typeName: 'Kalvekød'
    },
    {
      typeId: 5,
      typeName: 'Pølser'
    },
    {
      typeId: 6,
      typeName: 'Røvarer'
    },
    {
      typeId: 7,
      typeName: 'Lam'
    },
    {
      typeId: 8,
      typeName: 'Hakkekød'
    },
    {
      typeId: 9,
      typeName: 'Færdigretter'
    },
    {
      typeId: 10,
      typeName: 'Pålæg'
    }
  ];
}
