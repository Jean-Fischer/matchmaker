import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatchDto, MatchService, PlayerDto, PlayerService } from 'generated-sources/openapi';

@Component({
  templateUrl: './match-creator-page.component.html',
  styleUrls: ['./match-creator-page.component.scss']
})
export class MatchCreatorPageComponent implements OnInit {


  public players: PlayerDto[] = [];
  public selectedPlayer1!: PlayerDto | undefined;
  public selectedPlayer2!: PlayerDto | undefined;

  constructor(private matchService: MatchService, private playerService: PlayerService) {
   

  }
  ngOnInit(): void {
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

  public resetMatchCreator(): void {
    this.selectedPlayer1 = undefined;
    this.selectedPlayer2 = undefined;
  }

 

  public refreshPlayers(): void {
    this.playerService.apiPlayerGet().subscribe(s => this.players = s);
  }

  newPlayerForm = new FormGroup({
    nickName: new FormControl('', Validators.required),
    rank: new FormControl(1200, Validators.required),
  });



  onSubmit(): void {
    console.warn(this.newPlayerForm.value);
    let value = this.newPlayerForm.value as PlayerDto;
    this.playerService.apiPlayerPost(value).subscribe(() => this.refreshPlayers());
  }

}
