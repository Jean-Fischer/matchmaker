import { Component, OnInit } from '@angular/core';
import { MatchQueueDto, MatchQueueService, PlayerDto, PlayerService } from 'generated-sources/openapi';
import { ToastrService } from 'ngx-toastr';
import { Observable, Subject } from 'rxjs';

@Component({
  templateUrl: './queue-page.component.html',
  styleUrls: ['./queue-page.component.scss']
})
export class QueuePageComponent implements OnInit {
  public players$!: Observable<PlayerDto[]>;
  public matchQueues$!:Observable<MatchQueueDto[]>;
  public reloadMatchList: Subject<boolean> = new Subject();
  constructor(private playerService: PlayerService, private matchqueueService: MatchQueueService,private toastr: ToastrService) { }

  ngOnInit(): void {
    this.players$=this.playerService.apiPlayerGet();
    this.refreshQueue();
  }

  public addPlayer(player: PlayerDto) {
    this.matchqueueService.apiMatchQueuePut(player.id).subscribe(() => {this.toastr.success("Player added");this.refreshQueue()});
  }
  public refreshQueue(){
    this.matchQueues$ = this.matchqueueService.apiMatchQueueGet();
  }

  public process(){
    this.matchqueueService.apiMatchQueueProcessPost().subscribe(() => { this.refreshQueue(); this.toastr.success("Queue processed"); this.triggerReloadMatchList()});
  }
  public triggerReloadMatchList(){
    this.reloadMatchList.next(true);
  }
}
