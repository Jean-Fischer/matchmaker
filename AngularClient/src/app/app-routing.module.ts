import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { MatchCreatorPageComponent } from './match-creator-page/match-creator-page.component';
import { MatchCreatorComponent } from './match-creator/match-creator.component';
import { QueuePageComponent } from './queue-page/queue-page.component';

const routes: Routes = [{ path: 'queue', component: QueuePageComponent }, { path: '', component:MatchCreatorPageComponent }];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
