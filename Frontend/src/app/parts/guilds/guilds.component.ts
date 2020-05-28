import { Component, OnInit } from '@angular/core';
import { Guild } from 'src/app/models/guild';
import { GuildService } from 'src/app/services/GuildService/guild.service';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { CreateguildComponent } from '../createguild/createguild.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-guilds',
  templateUrl: './guilds.component.html',
  styleUrls: ['./guilds.component.css']
})
export class GuildsComponent implements OnInit {
  guilds: Guild[]

  constructor(private guildService: GuildService, 
    private dialog: MatDialog,
    private router: Router) { }

  ngOnInit(): void {
    this.loadAllGuilds();
  }

  loadAllGuilds(){
    console.log("Reload all guilds")
    this.guildService.getAllGuilds().subscribe(
      data => {
        this.guilds = data;

        this.guilds = this.guilds.sort(function (a, b) {
          return new Date(a.creationDate).getTime() - new Date(b.creationDate).getTime();
        });
      },
      error => {
        console.error(error);
      });
  }

  onSelect(guild: Guild) {
    this.router.navigate(['./guild', guild.id]);
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
      if (result){
        this.loadAllGuilds()
        this.onSelect(result);
      }
    });
  }

}
