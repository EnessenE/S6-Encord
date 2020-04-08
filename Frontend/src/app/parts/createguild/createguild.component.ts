import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { environment } from 'src/environments/environment';
import { GuildService } from 'src/app/services/GuildService/guild.service';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-createguild',
  templateUrl: './createguild.component.html',
  styleUrls: ['./createguild.component.css']
})
export class CreateguildComponent implements OnInit {
  errorText: string;
  guildForm: FormGroup;
  submitted: boolean;

  constructor(private formBuilder: FormBuilder,
    private guildService: GuildService,
    public dialogRef: MatDialogRef<CreateguildComponent>) { }

  ngOnInit(): void {
    this.guildForm = this.formBuilder.group({
      name: ['', Validators.required],
    });
  }

  get f() { return this.guildForm.controls; }

  onSubmit(){
    this.guildService.createGuild(this.guildForm.value).subscribe(
      data => {
        this.close(data);
      },
      error => {
        this.errorText = error;
      });
    this.close(null);
  }

  close(sendBack): void {
    this.dialogRef.close(sendBack);
  }
}
