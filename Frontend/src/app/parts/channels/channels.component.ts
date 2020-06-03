import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Channel } from 'src/app/models/channel';
import { ActivatedRoute, Router } from '@angular/router';
import { ChannelService } from 'src/app/services/ChannelService/channel.service';
import { Guild } from 'src/app/models/guild';
import { MatDialogConfig, MatDialog } from '@angular/material/dialog';
import { CreatechannelComponent } from '../createchannel/createchannel.component';
import { faCog, faHeadphonesAlt, faAlignLeft } from '@fortawesome/free-solid-svg-icons';
import { ChannelsettingsComponent } from '../channelsettings/channelsettings.component';

@Component({
  selector: 'app-channels',
  templateUrl: './channels.component.html',
  styleUrls: ['./channels.component.css']
})
export class ChannelsComponent implements OnInit {
  channels: Channel[];
  currentId;  
  hoverIndex: number = -1;

  //icons
  faHeadphonesAlt = faHeadphonesAlt;
  faAlignLeft = faAlignLeft;
  faCog = faCog;

  @Output()
  selectedChannel: EventEmitter<Channel> = new EventEmitter<Channel>();

  constructor(private route: ActivatedRoute,
    private channelService: ChannelService,
    private dialog: MatDialog,
    private router: Router) { }

  ngOnInit(): void {
    this.getChannels()
  }

  channelSettings(channel: Channel) {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = false;
    dialogConfig.autoFocus = true;
    dialogConfig.width = '80%';
    dialogConfig.height = '80%';
    dialogConfig.data = channel;

    let dialogRef = this.dialog.open(ChannelsettingsComponent, dialogConfig);
    dialogRef.afterClosed().subscribe(result => {
      if (result != null && result == true) {
        this.getChannels();
        //remove channel from list
      }
    });
  }


  getFirstTextChannel(): Channel{
    console.log("Finding a channel to select");
    var channel = null;
    this.channels.forEach(element => {
      if (element.type == "TextChannel"){
        channel = element;
      }
    });
    return channel;
  }
  
  getChannels() {
    console.log("Retrieving all channels again");
    const id = this.route.snapshot.paramMap.get('id');
    this.currentId = id;
    this.channelService.getAllChannelsOnGuild(id.toString()).subscribe(
      data => {
        this.channels = data;
        this.selectedChannel.emit(this.getFirstTextChannel())
      },
      error => {
        console.error(error);
      });
  }

  onHover(i: number) {
    this.hoverIndex = i;
  }

  onSelect(channel: Channel) {
    this.selectedChannel.emit(channel);
  }

  createChannel() {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = false;
    dialogConfig.autoFocus = true;
    dialogConfig.width = '400px';
    dialogConfig.height = '600px';
    dialogConfig.data = this.currentId;

    let dialogRef = this.dialog.open(CreatechannelComponent, dialogConfig);
    dialogRef.afterClosed().subscribe(result => {
      this.getChannels()
      if (result) {
        this.onSelect(result);
      }
    });
  }

}
