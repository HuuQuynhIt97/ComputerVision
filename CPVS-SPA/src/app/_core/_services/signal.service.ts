import { environment } from './../../../environments/environment';
import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
@Injectable({
  providedIn: 'root'
})
export class SignalService {

  base_hub: any = environment.hubCPVS
  private hubConnection: signalR.HubConnection
  constructor() { }

  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(this.base_hub)
      .build();
    this.hubConnection.start().then(() => console.log('Connection started')).catch(err =>{
      })
  }
  public ReceiveMessage = () => {
    this.hubConnection.on('Welcom', (message, line) => {
      // console.log(message);
      // console.log(line);
    });
  }

}
