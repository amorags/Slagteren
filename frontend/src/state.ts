import {Injectable} from "@angular/core";
import {User, Product} from "./models";

@Injectable({
  providedIn: 'root'
})
export class State {
  products: Product[] = [];
  users: User[] = [];
}
