import { Component, Input, OnInit } from '@angular/core';
import { Player } from 'generated-sources/openapi';

@Component({
  selector: 'app-player-card',
  templateUrl: './player-card.component.html',
  styleUrls: ['./player-card.component.scss']
})
export class PlayerCardComponent implements OnInit {

  @Input() player: Player = {id: 1, nickname: "Testos", rank : 1200};

  constructor() { }

  ngOnInit(): void {
  }

}
