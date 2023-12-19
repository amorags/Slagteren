import { Component, OnInit } from '@angular/core';
import {FormBuilder, Validators} from "@angular/forms";
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {ToastController} from "@ionic/angular";
import { State } from 'src/state';
import {firstValueFrom} from "rxjs";
import { environment } from 'src/environments/environment.prod';
import { ResponseDto, User } from 'src/models';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent  implements OnInit {

  loginForm = this.fb.group({

    email: ['', Validators.required],
    password: ['', Validators.required, Validators.minLength(8)]

  })

  constructor(public http: HttpClient, public state: State, public fb: FormBuilder, public toastController: ToastController) { }

  ngOnInit() {}

  async submit() {


    try {

      const observable = this.http.post<ResponseDto<User>>(
        environment.baseUrl + '/api/account/login',
        this.loginForm.getRawValue()
      );
      
      const response = await firstValueFrom<ResponseDto<User>>(observable);
      this.state.users.push(response.responseData!);

      const toast = await this.toastController.create({
        message: 'Du er nu logget ind',
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
