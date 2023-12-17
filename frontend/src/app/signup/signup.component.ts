import { Component, OnInit } from '@angular/core';
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {State} from "../../state";
import {environment} from "../../environments/environment.prod";
import {firstValueFrom} from "rxjs";
import {User, ResponseDto} from '../../models'
import {AbstractControl, FormBuilder, ReactiveFormsModule, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import {ToastController} from "@ionic/angular";
import { Router } from '@angular/router';
const passwordValidator: ValidatorFn = (control: AbstractControl): ValidationErrors | null => {
  const value: string = control.value;
  const hasUpperCase = /[A-Z]/.test(value);
  const hasLowerCase = /[a-z]/.test(value);
  const hasNumber = /\d/.test(value);
  const hasSpecialChar = /[!@#$%^&*(),.?":{}|<>]/.test(value);

  const isValid = hasUpperCase && hasLowerCase && hasNumber && hasSpecialChar;

  return isValid ? null : { passwordRequirements: true };
};


@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss'],
  })
export class SignupComponent  implements OnInit {


  createNewUser = this.fb.group({
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    address: ['', Validators.required],
    zip: [, [Validators.maxLength(4), Validators.required]],
    city: ['', Validators.required],
    country: ['', Validators.required],
    phone: [, [Validators.minLength(8), Validators.required]],
    password: ['', [Validators.minLength(8), Validators.required, passwordValidator]]
  });



  constructor(public Http: HttpClient, public state: State, public fb: FormBuilder, public toastController: ToastController, public router: Router) {
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

      setTimeout(() => {
        this.router.navigate(['/login']);
      }, 1000);

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
