import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {State} from "../../state";
import {Product, ResponseDto} from "../../models";
import {environment} from "../../environments/environment.prod";
import {firstValueFrom} from "rxjs";

@Component({
  selector: 'app-list-customer',
  templateUrl: './list-customer.component.html',
  styleUrls: ['./list-customer.component.scss'],
})
export class ListCustomerComponent  implements OnInit {

  constructor(public Http: HttpClient, public state: State) {

  }

  async fetchCustomer()
  {
    const result = await firstValueFrom(this.Http.get<ResponseDto<Product[]>>(environment.baseUrl + '/api/customers'))
    this.state.products = result.responseData!;
  }

  ngOnInit() {
    this.fetchCustomer()
  }

  protected readonly State = State;
}
