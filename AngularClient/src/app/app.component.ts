import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Match, MatchDto, MatchService, PlayerDto, PlayerService } from 'generated-sources/openapi';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  public matches: MatchDto[] = [];
  public players: PlayerDto[] = [];
  public selectedPlayer1!: PlayerDto | undefined;
  public selectedPlayer2!: PlayerDto | undefined;

  constructor(private matchService: MatchService, private http: HttpClient, private playerService: PlayerService) {
    matchService.apiMatchGet().subscribe(s => this.matches = s)
    playerService.apiPlayerGet().subscribe(s => this.players = s);

  }


  public addPlayer(player: PlayerDto) {
    if (!this.selectedPlayer1)
      this.selectedPlayer1 = player;
    else {
      if (!this.selectedPlayer2)
        this.selectedPlayer2 = player;
    }
  }

  public resetMatchCreator():void{
    this.selectedPlayer1 = undefined;
    this.selectedPlayer2 = undefined;
  }

}
