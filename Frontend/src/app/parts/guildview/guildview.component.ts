import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, NavigationEnd } from '@angular/router';
import { GuildService } from 'src/app/services/GuildService/guild.service';
import { Guild } from 'src/app/models/guild';
import { GuildsettingsComponent } from '../guildsettings/guildsettings.component';
import { MatDialogConfig, MatDialog } from '@angular/material/dialog';
import { Channel } from 'src/app/models/channel';

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
    private router: Router) { }

  ngOnInit(): void {
    this.getGuild()
    this.router.events.subscribe((val) => {
      this.getGuild();
    });
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
        },
        error => {
          console.error(error);
        });
    }
  }

}
