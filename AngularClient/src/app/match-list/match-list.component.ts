import { Component, Input, OnInit } from '@angular/core';
import { Request } from 'generated-sources/grpc/match.pb';
import { MatchGprcServiceClient } from 'generated-sources/grpc/match.pbsc';
import { MatchDto, MatchService } from 'generated-sources/openapi';
import { map, Observable, Subject } from 'rxjs';
import { MatchHub } from '../signalR/matchHub';

@Component({
  selector: 'app-match-list',
  templateUrl: './match-list.component.html',
  styleUrls: ['./match-list.component.scss']
})
export class MatchListComponent implements OnInit {
  public matches$!: Observable<MatchDto[]>;
  @Input()
  reloading: Subject<boolean> = new Subject<boolean>();


  constructor(private matchService: MatchService, private matchHub: MatchHub, private grpcService : MatchGprcServiceClient) { }

  ngOnInit(): void {
    this.refreshMatches();
    
  }
  public refreshMatches(): void {
    //SignalR endpoint
    this.matches$ = this.matchHub.getAllStream();
    //Grpc live endpoint
    this.matches$ = this.grpcService.getAllRefreshed(new Request()).pipe(map(s=>s.result as MatchDto[]));
    
  }
}
