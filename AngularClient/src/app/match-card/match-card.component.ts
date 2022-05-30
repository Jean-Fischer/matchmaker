import { Component, Input, OnInit } from '@angular/core';
import { MatchDto } from 'generated-sources/openapi';

@Component({
  selector: 'app-match-card',
  templateUrl: './match-card.component.html',
  styleUrls: ['./match-card.component.scss']
})
export class MatchCardComponent implements OnInit {
  @Input()
  match!: MatchDto;

  constructor() { }

  ngOnInit(): void {
  }

}
