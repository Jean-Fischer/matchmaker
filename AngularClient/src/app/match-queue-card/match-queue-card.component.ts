import { Component, Input, OnInit } from '@angular/core';
import { MatchQueueDto } from 'generated-sources/openapi';

@Component({
  selector: 'app-match-queue-card',
  templateUrl: './match-queue-card.component.html',
  styleUrls: ['./match-queue-card.component.scss']
})
export class MatchQueueCardComponent implements OnInit {

  @Input() matchQueue!:MatchQueueDto;
  
  constructor() { }

  ngOnInit(): void {
  }

}
