import { Injectable, OnInit, EventEmitter } from '@angular/core';
import * as signalR from "@aspnet/signalr";
import { Message } from 'src/app/models/message';
import { IHttpConnectionOptions } from '@aspnet/signalr';
import { AuthenticationService } from '../AuthService/auth.service';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  messageReceived = new EventEmitter<Message>();
  connectionEstablished = new EventEmitter<Boolean>();
  
  private options: IHttpConnectionOptions;
  private connectionIsEstablished = false;
  private _hubConnection: signalR.HubConnection;

  constructor(private authService: AuthenticationService) {
    this.options = {
      accessTokenFactory: function () {
        return authService.getToken();
      }
    };
    this.createConnection();
    this.registerOnServerEvents();
    this.startConnection();
  }

  sendMessage(message: Message) {
    this._hubConnection.invoke('NewMessage', message);
  }



  private createConnection() {
    this._hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:7031/ws/chat', this.options)
      .build();
  }

  private startConnection(): void {
    this._hubConnection
      .start()
      .then(() => {
        this.connectionIsEstablished = true;
        console.log('Hub connection started');
        this.connectionEstablished.emit(true);
      })
      .catch(err => {
        console.log('Error while establishing connection, retrying...');
        this.connectionEstablished.emit(false);
        setTimeout(function () { this.startConnection(); }, 5000);
      });
  }

  private registerOnServerEvents(): void {
    this._hubConnection.on('MessageReceived', (data: any) => {
      this.messageReceived.emit(data);
    });
  }
}