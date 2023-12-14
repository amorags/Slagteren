import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {State} from "../../state";
import {environment} from "../../environments/environment.prod";
import {firstValueFrom} from "rxjs";
import {User, ResponseDto} from '../../models'
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss'],
})
export class SignupComponent  implements OnInit {


  createNewUser = this.fb.group({
    firstName: ['', ],
    lastName: [''],
    email: [''],
    address: [''],
    zip: [0],
    city: [''],
    country: [''],
    phone: [0],
    password: ['']
  })


  constructor(public Http: HttpClient, public state: State, public fb: FormBuilder) {
  }

  ngOnInit() {}

  async submit() {
    const observable = this.Http.post<ResponseDto<User>>(
      environment.baseUrl + '/api/account/register',
      this.createNewUser.getRawValue()
    );
    const response = await firstValueFrom<ResponseDto<User>>(observable);
    this.state.users.push(response.responseData!);
  }

}
