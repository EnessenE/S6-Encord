import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GuildviewComponent } from './guildview.component';

describe('GuildviewComponent', () => {
  let component: GuildviewComponent;
  let fixture: ComponentFixture<GuildviewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GuildviewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GuildviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
