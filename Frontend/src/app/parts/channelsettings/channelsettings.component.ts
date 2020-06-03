import { Component, OnInit, Inject } from '@angular/core';
import { Channel } from 'src/app/models/channel';
import { ChannelService } from 'src/app/services/ChannelService/channel.service';
import { faDumpsterFire } from '@fortawesome/free-solid-svg-icons';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-channelsettings',
  templateUrl: './channelsettings.component.html',
  styleUrls: ['./channelsettings.component.css']
})
export class ChannelsettingsComponent implements OnInit {
  channel: Channel;
  faBin = faDumpsterFire;

  constructor(private channelService: ChannelService,
    public dialogRef: MatDialogRef<ChannelsettingsComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Channel) { }

  ngOnInit(): void {
    this.channel = this.data;
  }

  delete(){
    this.channelService.deleteChannel(this.channel).subscribe(data => {
      if (data != null && data == true) {
        console.log("Succesfully deleted")
      };
    });
    this.close(true);
  }

  close(sendBack): void {
    this.dialogRef.close(sendBack);
  }
}
