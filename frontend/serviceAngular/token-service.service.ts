import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TokenServiceService {

  private readonly storage = window.sessionStorage;

  constructor() {
  }

  public setToken(token: string){
    this.storage.setItem("token", token);
  }

  public gettoken(){
    return this.storage.getItem("token");
  }
}
