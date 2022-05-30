import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MatchCreatorComponent } from './match-creator.component';

describe('MatchCreatorComponent', () => {
  let component: MatchCreatorComponent;
  let fixture: ComponentFixture<MatchCreatorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MatchCreatorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MatchCreatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
