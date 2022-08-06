import { Component, Input, OnInit } from '@angular/core';
import { Request } from 'generated-sources/grpc/match.pb';
import { MatchGprcServiceClient } from 'generated-sources/grpc/match.pbsc';
import { MatchDto, MatchService } from 'generated-sources/openapi';
import { map, Observable, Subject } from 'rxjs';
import { GraphQLService } from '../services/graphQL/graphQL.service';
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


  constructor(private matchService: MatchService, private matchHub: MatchHub, private grpcService : MatchGprcServiceClient, private graphQLService : GraphQLService) { }

  ngOnInit(): void {
    this.refreshMatches();
    
  }
  public refreshMatches(): void {
    //SignalR endpoint
    this.matches$ = this.matchHub.getAllStream();
    //Grpc live endpoint
    this.matches$ = this.grpcService.getAllRefreshed(new Request()).pipe(map(s=>s.result as MatchDto[]));
    //let's try to do the same with graphQL
    this.graphQLService.GetBasicMatchesInfo().subscribe(s=>console.log(s));

    
  }
}
