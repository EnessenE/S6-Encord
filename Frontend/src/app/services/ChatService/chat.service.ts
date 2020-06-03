import { Injectable, OnInit, EventEmitter } from '@angular/core';
import * as signalR from "@aspnet/signalr";
import { Message } from 'src/app/models/message';
import { IHttpConnectionOptions } from '@aspnet/signalr';
import { AuthenticationService } from '../AuthService/auth.service';
import { NgxBootstrapAlertNotificationService } from '@benevideschissanga/ngx-bootstrap-alert-notification';
import { Channel } from 'src/app/models/channel';
import { Guild } from 'src/app/models/guild';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  messageReceived = new EventEmitter<Message>();
  deletionReceived = new EventEmitter<Channel>();
  guildDeletion = new EventEmitter<Guild>();
  connectionEstablished = new EventEmitter<Boolean>();

  private options: IHttpConnectionOptions;
  private connectionIsEstablished = false;
  private _hubConnection: signalR.HubConnection;

  constructor(private authService: AuthenticationService,
    private notificationService: NgxBootstrapAlertNotificationService) {
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
    this._hubConnection.on('ChannelDeleted', (channel: Channel) => {
      this.deletionReceived.emit(channel);
      this.notificationService.show(
        {
          type: 'info',
          message: 'The channel ' + channel.name + " was deleted. \n All messages sent have been lost.",
          icon: 'icon icon-bell-55',
          title: 'A channel was deleted',
        },
        {
          position: 'topRight',
        }
      )
    });
    this._hubConnection.on('GuildDeleted', (guild: Guild) => {
      this.guildDeletion.emit(guild);
      this.notificationService.show(
        {
          type: 'info',
          message: 'The guild ' + guild.name + " was deleted. \n All messages sent have been lost.",
          icon: 'icon icon-bell-55',
          title: 'A guild was deleted',
        },
        {
          position: 'topRight',
        }
      )
    });
  }
}