import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, NavigationEnd } from '@angular/router';
import { GuildService } from 'src/app/services/GuildService/guild.service';
import { Guild } from 'src/app/models/guild';
import { GuildsettingsComponent } from '../guildsettings/guildsettings.component';
import { MatDialogConfig, MatDialog } from '@angular/material/dialog';
import { Channel } from 'src/app/models/channel';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-guildview',
  templateUrl: './guildview.component.html',
  styleUrls: ['./guildview.component.css']
})
export class GuildviewComponent implements OnInit {
  guild: Guild;
  currentSearchId: string;
  targetTextChannel: Channel;

  constructor(private route: ActivatedRoute,
    private dialog: MatDialog,
    private guildService: GuildService,
    private router: Router,
    private titleService: Title) { }

  ngOnInit(): void {
    this.getGuild()
    this.router.events.subscribe((val) => {
      this.getGuild();
    });
  }

  SelectChannel($event) {
    const channel = $event;
    if (channel) {
      if (channel.type == "TextChannel") {
        this.targetTextChannel = $event;
      }
      console.log("Changed channel to", this.targetTextChannel.name)
    }
    else {
      console.log("No channel selected");
    }
  }

  GuildSettings() {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = false;
    dialogConfig.autoFocus = true;
    dialogConfig.width = '80%';
    dialogConfig.height = '80%';
    dialogConfig.data = this.guild;

    let dialogRef = this.dialog.open(GuildsettingsComponent, dialogConfig);
    dialogRef.afterClosed().subscribe(result => {
      if (result != null && result == true) {
        this.guild = null;
        this.router.navigate(['./dashboard']);
      }
    });
  }

  getGuild() {
    const id = this.route.snapshot.paramMap.get('id');
    if (this.currentSearchId != id) {
      this.currentSearchId = id;
      this.guild = null;
      this.guildService.getGuild(id).subscribe(
        data => {
          this.guild = data;
          this.titleService.setTitle(data.name)
        },
        error => {
          console.error(error);
        });
    }
  }

}
