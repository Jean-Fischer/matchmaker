import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Match, MatchDto, MatchService, PlayerDto, PlayerService } from 'generated-sources/openapi';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'match-maker';
  public match : MatchDto = {};
  public players : PlayerDto[] = [];
  constructor(private matchService : MatchService, private http : HttpClient, private playerService : PlayerService){
    matchService.apiMatchMatchidGet(6).subscribe(s=>this.match = s)
    playerService.apiPlayerGet().subscribe(s=>this.players = s);
    
  }

  
}
