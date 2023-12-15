import { Component, OnInit } from '@angular/core';
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {State} from "../../state";
import {environment} from "../../environments/environment.prod";
import {firstValueFrom} from "rxjs";
import {User, ResponseDto} from '../../models'
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import {ToastController} from "@ionic/angular";

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss'],
  })
export class SignupComponent  implements OnInit {


  createNewUser = this.fb.group({
     firstName: ['', Validators.required],
    lastName: ['', Validators.required],
    email: ['', Validators.required],
    address: ['', Validators.required],
    zip: [0, Validators.maxLength(4)],
    city: ['', Validators.required],
    country: ['', Validators.required],
    phone: [0, Validators.minLength(8)],
    password: ['', Validators.minLength(8)]
  })


  constructor(public Http: HttpClient, public state: State, public fb: FormBuilder, public toastController: ToastController) {
  }

  ngOnInit() {}

  async submit() {

    const name = this.createNewUser.getRawValue().firstName;

    try {

      const observable = this.Http.post<ResponseDto<User>>(
        environment.baseUrl + '/api/account/register',
        this.createNewUser.getRawValue()
      );
      const response = await firstValueFrom<ResponseDto<User>>(observable);
      this.state.users.push(response.responseData!);

      const toast = await this.toastController.create({
        message: 'Velkommen ' + name + ' du er nu oprettet i vores system',
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
