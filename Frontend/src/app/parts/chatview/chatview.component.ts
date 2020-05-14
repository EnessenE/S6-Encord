import { Component, OnInit, Input, NgZone } from '@angular/core';
import { Channel } from 'src/app/models/channel';
import { ChatService } from 'src/app/services/ChatService/chat.service';
import { Message } from 'src/app/models/message';
import { ChannelService } from 'src/app/services/ChannelService/channel.service';

@Component({
  selector: 'app-chatview',
  templateUrl: './chatview.component.html',
  styleUrls: ['./chatview.component.css']
})
export class ChatviewComponent implements OnInit {
  @Input()
  channel: Channel;

  ngOnInit(): void {
    this.retrieveData()
  }

  constructor(
    private chatService: ChatService,
    private channelService: ChannelService,
    private _ngZone: NgZone) {
    this.subscribeToEvents();
  }

  title = 'ClientApp';
  txtMessage: string = '';
  uniqueID: string = new Date().getTime().toString();
  messages;
  message = new Message();


  retrieveData() {
    console.log("Retrieving data");
    if (this.channel) {
      this.channelService.getTextChannel(this.channel.id).subscribe(
        result => {
          this.channel = result;
          this.messages = result.messages;
        });
    }
    else {
      console.log("No channel to retrieve");
    }
  }

  sendMessage(): void {
    if (!this.messages){
      this.messages = new Array<Message>();
    }
    if (this.txtMessage) {
      this.message = new Message();
      this.message.clientuniqueid = this.uniqueID;
      this.message.type = "sent";
      this.message.content = this.txtMessage;
      this.message.channelId = this.channel.id;
      //this.messages.push(this.message);
      this.chatService.sendMessage(this.message);
      this.txtMessage = '';
    }
  }
  private subscribeToEvents(): void {
    this.chatService.messageReceived.subscribe((message: Message) => {
      this._ngZone.run(() => {
       // if (message.clientuniqueid !== this.uniqueID) {
          console.log("Message received")
          message.type = "received";
          if (!this.messages){
            this.messages = new Array<Message>();
          }
          this.messages.push(message);
       // }
      });
    });
  }

}
