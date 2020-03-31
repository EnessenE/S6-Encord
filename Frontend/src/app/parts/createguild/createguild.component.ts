import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-createguild',
  templateUrl: './createguild.component.html',
  styleUrls: ['./createguild.component.css']
})
export class CreateguildComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

  CreateGuild(){
    let dialogRef = dialog.open(CreateguildComponent, {
      height: '400px',
      width: '600px',
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`); // Pizza!
    });
  }
}
