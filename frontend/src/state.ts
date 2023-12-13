import {Injectable} from "@angular/core";
import {Customer, Product} from "./models";

@Injectable({
  providedIn: 'root'
})
export class State {
  products: Product[] = [];
  customers: Customer[] = [];
}
