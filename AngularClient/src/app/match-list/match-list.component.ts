import { Component, Input, OnInit } from '@angular/core';
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


  constructor(private matchService:MatchService,private matchHub:MatchHub) { }

  ngOnInit(): void {
    this.refreshMatches();
    //this.reloading.subscribe(()=>this.refreshMatches());
    
  }
  public refreshMatches(): void {
    //this.matches$ = this.matchService.apiMatchGet();
    // this.matchHub.signalRObservable().subscribe(s=>console.log(s));
    // this.matches$ = this.matchHub.signalRObservable();
    this.matches$ = this.matchHub.getAllStream();
    
  }
}
