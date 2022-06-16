import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MatchCreatorPageComponent } from './match-creator-page.component';

describe('MatchCreatorPageComponent', () => {
  let component: MatchCreatorPageComponent;
  let fixture: ComponentFixture<MatchCreatorPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MatchCreatorPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MatchCreatorPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
