import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {State} from "../../state";
import {User, ResponseDto} from "../../models";
import {environment} from "../../environments/environment.prod";
import {firstValueFrom} from "rxjs";

@Component({
  selector: 'app-list-customer',
  templateUrl: './list-user.component.html',
  styleUrls: ['./list-user.component.scss'],
})
export class ListUserComponent implements OnInit {

  constructor(public Http: HttpClient, public state: State) {

  }

  async fetchCustomer()
  {
    const result = await firstValueFrom(this.Http.get<ResponseDto<User[]>>(environment.baseUrl + '/api/users'))
    this.state.users = result.responseData!;
  }

  ngOnInit() {
    this.fetchCustomer()
  }

}
