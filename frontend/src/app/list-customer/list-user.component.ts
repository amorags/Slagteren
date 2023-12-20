import { Component, OnInit } from '@angular/core';
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {State} from "../../state";
import {User, ResponseDto} from "../../models";
import {environment} from "../../environments/environment.prod";
import {firstValueFrom} from "rxjs";
import {AlertController, ToastController} from "@ionic/angular";

@Component({
  selector: 'app-list-customer',
  templateUrl: './list-user.component.html',
  styleUrls: ['./list-user.component.scss'],
})
export class ListUserComponent implements OnInit {

  constructor(public Http: HttpClient,public alertController: AlertController,
              public state: State, public toastController: ToastController) {

  }

  async fetchCustomer()
  {
    const result = await firstValueFrom(this.Http.get<ResponseDto<User[]>>(environment.baseUrl + '/api/users'))
    this.state.users = result.responseData!;
  }

  ngOnInit() {
    this.fetchCustomer()
  }




  async deleteUser(userId: number | undefined) {
    const alert = await this.alertController.create({
      header: 'Bekræft sletning',
      message: 'Er du sikker på, at du vil slette denne bruger?',
      buttons: [
        {
          text: 'Cancel',
          role: 'cancel',
          handler: () => {
            console.log('Delete canceled');
          },
        },
        {
          text: 'Yes',
          handler: async () => {
            try {
              await firstValueFrom(this.Http.delete(environment.baseUrl + '/api/user/' + userId));
              this.state.users = this.state.users.filter((b) => b.userId != userId);
              const toast = await this.toastController.create({
                message: 'Bruger er blevet slettet',
                duration: 1233,
                color: 'success',
              });
              await toast.present();
            } catch (e) {
              console.log(e);
              if (e instanceof HttpErrorResponse) {
                const toast = await this.toastController.create({
                  message: e.error.messageToClient,
                  duration: 1233,
                  color: 'danger',
                });
                await toast.present();
              }
            }
          },
        },
      ],
    });

    await alert.present();
  }




}
