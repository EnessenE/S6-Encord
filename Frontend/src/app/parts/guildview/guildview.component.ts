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
  currentSearchId;

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
    const id = (+this.route.snapshot.paramMap.get('id')).toString();
    if (this.currentSearchId != id) {
      this.currentSearchId = id;
      this.guild = null;
      this.guildService.getGuild(id).subscribe(
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
