import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Match, MatchDto, MatchService } from 'generated-sources/openapi';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'match-maker';
  public match : MatchDto = {};
  constructor(private matchService : MatchService, private http : HttpClient){
    matchService.apiMatchMatchidGet(6).subscribe(s=>this.match = s)

    
  }

  
}
