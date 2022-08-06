import { Injectable } from "@angular/core";
import { Apollo, gql } from 'apollo-angular';
import { map, Observable } from "rxjs";

const GET_BASIC_MATCHES = gql`
query{
    dto{
      participationA {
        player {
          nickname
        }
      }
      participationB {
        player {
          nickname
        }
      }
      registrationDate
    }
  }
  `


@Injectable()
export class GraphQLService {

    constructor(private apollo: Apollo) {

    }
    public GetBasicMatchesInfo(): Observable<any> {
        this.apollo.watchQuery<any>({query:GET_BASIC_MATCHES,pollInterval:5000}).valueChanges.pipe(map(s=>s.data));
        return this.apollo.query<any>({query:GET_BASIC_MATCHES}).pipe(map(s=>s.data));
    }

}