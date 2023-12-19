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

  public getUserRole(): string | null {
    const token = this.gettoken();
    if (token) {
      // Decode the token to get user information
      const tokenPayload = JSON.parse(atob(token.split('.')[1]));
      return tokenPayload.role;
    }
    return null;
  }
}
