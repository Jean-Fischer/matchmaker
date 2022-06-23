import { Component, OnInit } from '@angular/core';
import { Request } from 'generated-sources/grpc/match.pb';
import { MatchGprcServiceClient } from 'generated-sources/grpc/match.pbsc';
import { MatchQueueDto, MatchQueueService, PlayerDto, PlayerService } from 'generated-sources/openapi';
import { ToastrService } from 'ngx-toastr';
import { Observable, Subject } from 'rxjs';

@Component({
  templateUrl: './queue-page.component.html',
  styleUrls: ['./queue-page.component.scss']
})
export class QueuePageComponent implements OnInit {
  public players$!: Observable<PlayerDto[]>;
  public unlistedPlayers$!: Observable<PlayerDto[]>;
  public matchQueues$!:Observable<MatchQueueDto[]>;
  public reloadMatchList: Subject<boolean> = new Subject();
  public test:Observable<Request> = new Observable<Request>();

  constructor(private playerService: PlayerService, private matchqueueService: MatchQueueService,private toastr: ToastrService, private matchClient : MatchGprcServiceClient) { }

  ngOnInit(): void {
    this.refreshPlayers();
    this.refreshQueue();
    this.matchClient.getAll(new Request).subscribe(s=>console.log("getAll", s));
    this.matchClient.getAllStream(new Request).subscribe(s=>console.log("getAllStream", s));
  }

  public addPlayer(player: PlayerDto) {
    this.matchqueueService.apiMatchQueuePut(player.id).subscribe(() => {this.toastr.success("Player added");this.refreshQueue();this.refreshPlayers();});
  }
  public refreshQueue(){
    this.matchQueues$ = this.matchqueueService.apiMatchQueueGet();
  }

  public refreshPlayers() {
    this.unlistedPlayers$ = this.playerService.apiPlayerUnlistedGet();
  }

  public process(){
    this.matchqueueService.apiMatchQueueProcessPost().subscribe(() => { this.refreshQueue(); this.refreshPlayers(); this.toastr.success("Queue processed"); this.triggerReloadMatchList()});
  }
  public triggerReloadMatchList(){
    this.reloadMatchList.next(true);
  }
}
