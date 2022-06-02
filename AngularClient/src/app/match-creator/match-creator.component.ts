import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatchService, PlayerDto } from 'generated-sources/openapi';

@Component({
  selector: 'app-match-creator',
  templateUrl: './match-creator.component.html',
  styleUrls: ['./match-creator.component.scss']
})
export class MatchCreatorComponent implements OnInit {
  @Input()
  player1:PlayerDto|undefined;

  @Input()
  player2:PlayerDto|undefined;
  constructor(private matchService : MatchService) { }

  @Output() onReset : EventEmitter<void> = new EventEmitter<void>();

  ngOnInit(): void {
  }


  public createGame():void {
    this.matchService.apiMatchCreatePost(this.player1?.id,this.player2?.id).subscribe(s=>console.log(s));
  }

  public reset():void{
    this.onReset.emit();
  }
}
