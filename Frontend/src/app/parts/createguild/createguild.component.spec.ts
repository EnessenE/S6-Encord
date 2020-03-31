import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateguildComponent } from './createguild.component';

describe('CreateguildComponent', () => {
  let component: CreateguildComponent;
  let fixture: ComponentFixture<CreateguildComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateguildComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateguildComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
