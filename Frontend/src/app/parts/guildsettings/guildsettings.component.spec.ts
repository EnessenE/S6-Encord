import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GuildsettingsComponent } from './guildsettings.component';

describe('GuildsettingsComponent', () => {
  let component: GuildsettingsComponent;
  let fixture: ComponentFixture<GuildsettingsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GuildsettingsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GuildsettingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
