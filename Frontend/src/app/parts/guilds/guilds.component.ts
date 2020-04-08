import { Component, OnInit } from '@angular/core';
import { Guild } from 'src/app/models/guild';
import { GuildService } from 'src/app/services/GuildService/guild.service';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { CreateguildComponent } from '../createguild/createguild.component';

@Component({
  selector: 'app-guilds',
  templateUrl: './guilds.component.html',
  styleUrls: ['./guilds.component.css']
})
export class GuildsComponent implements OnInit {
  guilds: Guild[]

  constructor(private guildService: GuildService, private dialog: MatDialog) { }

  ngOnInit(): void {
    this.loadAllGuilds();
  }

  loadAllGuilds(){
    this.guildService.getAllGuilds().subscribe(
      data => {
        this.guilds = data;
      },
      error => {
        console.error(error);
      });
  }

  onSelect(guild: Guild) {
    console.log("Trying to open: " + guild.name)
  }

  CreateGuild(){
    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = false;
    dialogConfig.autoFocus = true;
    dialogConfig.width = '400px';
    dialogConfig.height = '600px';
    
    let dialogRef = this.dialog.open(CreateguildComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
      this.loadAllGuilds()
      if (result != undefined){
        this.onSelect(result);
      }
    });
  }

}
