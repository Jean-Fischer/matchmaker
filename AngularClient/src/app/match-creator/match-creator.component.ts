import { Component, Input, OnInit } from '@angular/core';
import { MatchService, PlayerDto } from 'generated-sources/openapi';

@Component({
  selector: 'app-match-creator',
  templateUrl: './match-creator.component.html',
  styleUrls: ['./match-creator.component.scss']
})
export class MatchCreatorComponent implements OnInit {
  @Input()
  get player1():PlayerDto|undefined{return this._player1};
  set player1(player : PlayerDto| undefined){this._player1 = player};
  private _player1!: PlayerDto|undefined;

  @Input()
  get player2():PlayerDto|undefined{return this._player2};
  set player2(player : PlayerDto| undefined){this._player2 = player};
  private _player2!: PlayerDto|undefined;
  constructor(private matchService : MatchService) { }

  ngOnInit(): void {
  }


  public createGame():void {
    this.matchService.apiMatchCreatePost(this.player1?.id,this.player2?.id).subscribe(s=>console.log(s));
  }

  public reset():void{
    this.player1 = undefined;
    this.player2 = undefined;
  }
}
