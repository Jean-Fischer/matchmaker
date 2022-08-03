import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MatchQueueCardComponent } from './match-queue-card.component';

describe('MatchQueueCardComponent', () => {
  let component: MatchQueueCardComponent;
  let fixture: ComponentFixture<MatchQueueCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MatchQueueCardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MatchQueueCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
