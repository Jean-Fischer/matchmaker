import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { PlayerDto } from 'generated-sources/openapi';


@Component({
  selector: 'app-player-card',
  templateUrl: './player-card.component.html',
  styleUrls: ['./player-card.component.scss']
})
export class PlayerCardComponent implements OnInit {

  @Input() player: PlayerDto = {id: 1, nickname: "Testos", rank : 1200};

  @Output()
  onAdd: EventEmitter<PlayerDto> = new EventEmitter<PlayerDto>();

  constructor() { }

  ngOnInit(): void {
  }

  public add(){
    this.onAdd?.emit(this.player);
  }

}
