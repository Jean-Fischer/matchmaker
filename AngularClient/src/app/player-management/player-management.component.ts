import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';
import {  PlayerDto, PlayerService } from 'generated-sources/openapi';
import { Observable } from 'rxjs';
import { AppState } from '../state/app.state';
import { loadPlayers } from '../state/players/players.actions';
import { selectAllPlayers } from '../state/players/players.selectors';

@Component({
  selector: 'app-player-management',
  templateUrl: './player-management.component.html',
  styleUrls: ['./player-management.component.scss']
})
export class PlayerManagementComponent implements OnInit {


  public players$ = this.store.select(selectAllPlayers);

  constructor(private playerService: PlayerService, private store : Store<AppState>) {
   
    console.log("store",store)
  }
  ngOnInit(): void {
    this.refreshPlayers();
  }



 

  public refreshPlayers(): void {
    this.store.dispatch(loadPlayers());
    //this.players = this.playerService.apiPlayerGet();
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
