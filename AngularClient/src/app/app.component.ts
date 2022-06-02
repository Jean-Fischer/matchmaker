import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
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
    this.refreshMatches();
    this.refreshPlayers();

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

  public refreshMatches():void{
    this.matchService.apiMatchGet().subscribe(s => this.matches = s)
  }

  public refreshPlayers(): void {
    this.playerService.apiPlayerGet().subscribe(s => this.players = s);
  }

  newPlayerForm = new FormGroup({
    nickName: new FormControl('', Validators.required),
    rank: new FormControl(1200, Validators.required),
  });


  onSubmit():void{
    console.warn(this.newPlayerForm.value);
    let value = this.newPlayerForm.value as PlayerDto;
    this.playerService.apiPlayerPost(value).subscribe(()=>this.refreshPlayers());
  }

}
