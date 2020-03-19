import { Component, OnInit } from '@angular/core';
import { Guild } from 'src/app/models/guild';
import { GuildService } from 'src/app/services/GuildService/guild.service';

@Component({
  selector: 'app-guilds',
  templateUrl: './guilds.component.html',
  styleUrls: ['./guilds.component.css']
})
export class GuildsComponent implements OnInit {
  guilds: Guild[]

  constructor(private guildService: GuildService) { }

  ngOnInit(): void {
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
}
