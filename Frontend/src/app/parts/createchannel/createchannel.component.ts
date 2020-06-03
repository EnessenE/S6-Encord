import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { ChannelService } from 'src/app/services/ChannelService/channel.service';
import { MAT_DIALOG_DATA } from "@angular/material/dialog";
import { Channel } from 'src/app/models/channel';

@Component({
  selector: 'app-createchannel',
  templateUrl: './createchannel.component.html',
  styleUrls: ['./createchannel.component.css']
})
export class CreatechannelComponent implements OnInit {
  errorText: string;
  guildForm: FormGroup;
  channelTypes: string[];
  submitted: boolean;

   //currently simply the guildid

  constructor(private formBuilder: FormBuilder,
    private channelService: ChannelService,
    public dialogRef: MatDialogRef<CreatechannelComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
    ) { }

  ngOnInit(): void {
    this.guildForm = this.formBuilder.group({
      name: ['', Validators.required],
      type: ['', Validators.required]
    });

    this.channelService.getAllChannelTypes().subscribe(
      data => {
        this.channelTypes = data;
      })
  }

  get f() { return this.guildForm.controls; }

  onSubmit() {
    this.submitted = true
    
    var channel = this.guildForm.value as Channel;
    channel.guildID = this.data.toString();
    if (this.guildForm.valid) {
      this.channelService.createChannel(this.guildForm.value).subscribe(
        data => {
          this.close(data);
        },
        error => {
          this.errorText = error;
        });
    }
    else{
      //this.submitted = false
      console.log("Failed validation");
      return false;
    }
    //this.submitted = false
  }

  close(sendBack): void {
    this.dialogRef.close(sendBack);
  }

}
