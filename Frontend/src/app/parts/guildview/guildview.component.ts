import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, NavigationEnd } from '@angular/router';
import { GuildService } from 'src/app/services/GuildService/guild.service';
import { Guild } from 'src/app/models/guild';

@Component({
  selector: 'app-guildview',
  templateUrl: './guildview.component.html',
  styleUrls: ['./guildview.component.css']
})
export class GuildviewComponent implements OnInit {
  guild: Guild;

  constructor(private route: ActivatedRoute,
    private guildService: GuildService, 
    private router: Router) { }

  ngOnInit(): void {
    this.getGuild()
      this.router.events.subscribe((val) => {
        getGuild();
    });
  }

  getGuild(){
    const id = this.route.snapshot.paramMap.get('id');
    this.guildService.getGuild(id).subscribe(
      data => {
        this.guild = data;
      },
      error => {
        console.error(error);
      });
  }

}
