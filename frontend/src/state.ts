import {Injectable} from "@angular/core";
import {Product} from "./models";

@Injectable({
  providedIn: 'root'
})
export class State {
  products: Product[] = [];
}
