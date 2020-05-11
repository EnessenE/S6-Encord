import { Component, OnInit } from '@angular/core';
import { Channel } from 'src/app/models/channel';
import { ActivatedRoute, Router } from '@angular/router';
import { ChannelService } from 'src/app/services/ChannelService/channel.service';
import { Guild } from 'src/app/models/guild';
import { MatDialogConfig, MatDialog } from '@angular/material/dialog';
import { CreatechannelComponent } from '../createchannel/createchannel.component';
import { faCoffee, faHeadphonesAlt, faAlignLeft } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-channels',
  templateUrl: './channels.component.html',
  styleUrls: ['./channels.component.css']
})
export class ChannelsComponent implements OnInit {
  channels: Channel[];
  currentId;
  faHeadphonesAlt = faHeadphonesAlt;
  faAlignLeft = faAlignLeft;

  constructor(private route: ActivatedRoute,
    private channelService: ChannelService,
    private dialog: MatDialog,
    private router: Router) { }

  ngOnInit(): void {
    this.getChannels()
  }

  getChannels() {
    const id = this.route.snapshot.paramMap.get('id');
    this.currentId = id;
    this.channelService.getAllChannelsOnGuild(id.toString()).subscribe(
      data => {
        this.channels = data;
      },
      error => {
        console.error(error);
      });
  }

  onSelect(channel: Channel) {
    console.log("click " + channel.name);
  }

  CreateChannel() {
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
