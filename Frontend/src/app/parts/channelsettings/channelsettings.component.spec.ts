import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ChannelsettingsComponent } from './channelsettings.component';

describe('ChannelsettingsComponent', () => {
  let component: ChannelsettingsComponent;
  let fixture: ComponentFixture<ChannelsettingsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ChannelsettingsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ChannelsettingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
