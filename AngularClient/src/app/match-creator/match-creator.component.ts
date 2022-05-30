import { Component, Input, OnInit } from '@angular/core';
import { MatchService, PlayerDto } from 'generated-sources/openapi';

@Component({
  selector: 'app-match-creator',
  templateUrl: './match-creator.component.html',
  styleUrls: ['./match-creator.component.scss']
})
export class MatchCreatorComponent implements OnInit {
  @Input()
  player1!: PlayerDto;
  @Input()
  player2!: PlayerDto;
  constructor(private matchService : MatchService) { }

  ngOnInit(): void {
  }


  public createGame():void {
    this.matchService.apiMatchCreatePost(this.player1.id,this.player2.id).subscribe(s=>console.log(s));
  }

}
