import { Component, Input, OnInit } from '@angular/core';
import { MatchDto, MatchService } from 'generated-sources/openapi';
import { map, Observable, Subject } from 'rxjs';

@Component({
  selector: 'app-match-list',
  templateUrl: './match-list.component.html',
  styleUrls: ['./match-list.component.scss']
})
export class MatchListComponent implements OnInit {
  public matches$!: Observable<MatchDto[]>;
  @Input()
  reloading: Subject<boolean> = new Subject<boolean>();


  constructor(private matchService:MatchService) { }

  ngOnInit(): void {
    this.refreshMatches();
    // this.reloading.pipe(map(() => this.refreshMatches()));
    this.reloading.subscribe(()=>this.refreshMatches());
    
  }
  public refreshMatches(): void {
    this.matches$ = this.matchService.apiMatchGet();
  }
}
