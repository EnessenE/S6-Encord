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
        this.getGuild();
    });
  }

  getGuild() {
    const id = +this.route.snapshot.paramMap.get('id');
    if (!this.guild || this.guild.id != id.toString()) {
      this.guildService.getGuild(id.toString()).subscribe(
        data => {
          console.debug("got data")
          this.guild = data;
        },
        error => {
          console.error(error);
        });
    }
  }

}
