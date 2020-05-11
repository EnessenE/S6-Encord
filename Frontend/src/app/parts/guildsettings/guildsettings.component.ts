import { Component, OnInit, Inject } from '@angular/core';
import { GuildService } from 'src/app/services/GuildService/guild.service';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Guild } from 'src/app/models/guild';
import { faDumpsterFire } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-guildsettings',
  templateUrl: './guildsettings.component.html',
  styleUrls: ['./guildsettings.component.css']
})
export class GuildsettingsComponent implements OnInit {
  guild: Guild;
  faBin = faDumpsterFire;

  constructor(private guildService: GuildService,
    public dialogRef: MatDialogRef<GuildsettingsComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Guild) { }

  ngOnInit(): void {
    this.guild = this.data;
  }

  delete(){
    this.guildService.deleteGuild(this.guild).subscribe(data => {
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
