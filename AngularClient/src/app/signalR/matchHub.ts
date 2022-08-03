import {  Injectable } from "@angular/core";
import * as signalR from "@microsoft/signalr";
import { HubConnection } from "@microsoft/signalr";
import { MatchDto } from "generated-sources/openapi";
import { from, map, Observable, Subject } from "rxjs";

@Injectable()
export class MatchHub {
    constructor(){
    }

   

    public getAllStream(): Observable<MatchDto[]> {
        // Establishes a Hub Connection with specified url.
        const conn: HubConnection = new signalR.HubConnectionBuilder()
            .withUrl('https://localhost:7154/matchHub')
            .build();

        const subject: Subject<any> = new Subject<MatchDto[]>();

        // On reciving an event when the hub method with the specified method name is invoked
        conn.on('RefreshMatchList', (data: any[]) => {
            // Multicast the event.
            subject.next(data);
        });

        // When the connection is closed.
        conn.onclose((err?: Error) => {
            if (err) {
                // An error occurs
                subject.error(err);
            } else {
                // No more events to be sent.
                subject.complete();
            }
        });

        // Starts the connection.
        conn.start();

        // To be subscribed to by multiple components
        return subject;
    }

}
