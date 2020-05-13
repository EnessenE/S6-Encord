import { Component, OnInit, Input, NgZone } from '@angular/core';
import { Channel } from 'src/app/models/channel';
import { ChatService } from 'src/app/services/ChatService/chat.service';
import { Message } from 'src/app/models/message';

@Component({
  selector: 'app-chatview',
  templateUrl: './chatview.component.html',
  styleUrls: ['./chatview.component.css']
})
export class ChatviewComponent implements OnInit {
  @Input()
  channel: Channel;

  ngOnInit(): void {
  }

  constructor(
    private chatService: ChatService,
    private _ngZone: NgZone) {
    this.subscribeToEvents();
  }

  title = 'ClientApp';
  txtMessage: string = '';
  uniqueID: string = new Date().getTime().toString();
  messages = new Array<Message>();
  message = new Message();



  sendMessage(): void {
    if (this.txtMessage) {
      this.message = new Message();
      this.message.clientuniqueid = this.uniqueID;
      this.message.type = "sent";
      this.message.message = this.txtMessage;
      this.message.date = new Date();
      this.messages.push(this.message);
      this.chatService.sendMessage(this.message);
      this.txtMessage = '';
    }
  }
  private subscribeToEvents(): void {

    this.chatService.messageReceived.subscribe((message: Message) => {
      this._ngZone.run(() => {
        if (message.clientuniqueid !== this.uniqueID) {
          message.type = "received";
          this.messages.push(message);
        }
      });
    });
  }

}
