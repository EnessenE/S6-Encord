import { Component, OnInit, Input, NgZone, ViewChild, ElementRef } from '@angular/core';
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
  @ViewChild('chatBox') private myScrollContainer: ElementRef;
  txtMessage: string = '';
  uniqueID: string = new Date().getTime().toString();
  messages;
  message = new Message();
  connected: boolean;
  hoverIndex: number = -1;

  private _channel: Channel;
  @Input()
  set channel(val: Channel) {
    this._channel = val;
    this.retrieveData();
  }
  get channel() {
    return this._channel;
  }

  ngOnInit(): void {
    this.chatService.connectionEstablished.subscribe((state: boolean) => {
      this.connected = state;
    });
    this.scrollToBottom();
  }

  constructor(
    private chatService: ChatService,
    private channelService: ChannelService,
    private _ngZone: NgZone) {
  }

  scrollToBottom(): void {
    try {
      this.myScrollContainer.nativeElement.scrollTop = this.myScrollContainer.nativeElement.scrollHeight;
    } catch (err) {
    }
  }

  onHover(i: number) {
    this.hoverIndex = i;
  }

  retrieveData() {
    console.log("Retrieving data");
    if (this._channel) {
      var id = this._channel.id;
      this._channel = null;
      this.channelService.getTextChannel(id).subscribe(
        result => {
          this._channel = result;
          this.messages = result.messages;

          this.messages = this.messages.sort(function (a, b) {
            return new Date(a.createdAt).getTime() - new Date(b.createdAt).getTime();
          });
          this.subscribeToEvents();
          this.scrollToBottom();
        });
    }
    else {
      console.log("No channel to retrieve");
    }
  }

  sendMessage(): void {
    if (!this.messages) {
      this.messages = new Array<Message>();
    }
    if (this.txtMessage) {
      this.message = new Message();
      this.message.clientuniqueid = this.uniqueID;
      this.message.type = "sent";
      this.message.content = this.txtMessage;
      this.message.channelId = this._channel.id;
      //this.messages.push(this.message);
      this.chatService.sendMessage(this.message);
      this.scrollToBottom()
      this.txtMessage = '';
    }
  }
  private subscribeToEvents(): void {
    var channelid = this._channel.id;
    this.chatService.messageReceived.subscribe((message: Message) => {
      if (channelid != this._channel.id) {
        this.chatService.messageReceived.unsubscribe
      }
      this._ngZone.run(() => {
        // if (message.clientuniqueid !== this.uniqueID) {
        console.log("Message received")
        message.type = "received";
        if (!this.messages) {
          this.messages = new Array<Message>();
        }
        this.messages.push(message);
        this.scrollToBottom();
        // }
      });
    });
  }

}
