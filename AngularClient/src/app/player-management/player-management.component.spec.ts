import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlayerManagementComponent } from './player-management.component';

describe('PlayerManagementComponent', () => {
  let component: PlayerManagementComponent;
  let fixture: ComponentFixture<PlayerManagementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PlayerManagementComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PlayerManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
