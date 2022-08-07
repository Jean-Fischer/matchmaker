import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { EasyDataComponent } from './easydata/easydata.component';
import { MatchCreatorPageComponent } from './match-creator-page/match-creator-page.component';
import { MatchCreatorComponent } from './match-creator/match-creator.component';
import { PlayerManagementComponent } from './player-management/player-management.component';
import { QueuePageComponent } from './queue-page/queue-page.component';

const routes: Routes = [{ path: 'queue', component: QueuePageComponent }, 
{ path: 'player-management', component: PlayerManagementComponent },
{ path: 'easydata', component: EasyDataComponent },
{ path: '', component: MatchCreatorPageComponent }];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
