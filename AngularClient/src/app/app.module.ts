import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ApiModule, BASE_PATH } from 'generated-sources/openapi';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ApiModule,
    HttpClientModule
  ],
  providers: [{provide : BASE_PATH,useValue:"http://localhost:5154"}],
  bootstrap: [AppComponent]
})
export class AppModule { }
