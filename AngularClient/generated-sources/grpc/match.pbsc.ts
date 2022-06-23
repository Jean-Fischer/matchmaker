/* tslint:disable */
/* eslint-disable */
// @ts-nocheck
//
// THIS IS A GENERATED FILE
// DO NOT MODIFY IT! YOUR CHANGES WILL BE LOST
import { Inject, Injectable, Optional } from '@angular/core';
import {
  GrpcCallType,
  GrpcClient,
  GrpcClientFactory,
  GrpcEvent,
  GrpcMetadata
} from '@ngx-grpc/common';
import {
  GRPC_CLIENT_FACTORY,
  GrpcHandler,
  takeMessages,
  throwStatusErrors
} from '@ngx-grpc/core';
import { Observable } from 'rxjs';
import * as thisProto from './match.pb';
import * as googleProtobuf000 from '@ngx-grpc/well-known-types';
import { GRPC_MATCH_GPRC_SERVICE_CLIENT_SETTINGS } from './match.pbconf';
/**
 * Service client implementation for MatchGprcService
 */
@Injectable({ providedIn: 'any' })
export class MatchGprcServiceClient {
  private client: GrpcClient<any>;

  /**
   * Raw RPC implementation for each service client method.
   * The raw methods provide more control on the incoming data and events. E.g. they can be useful to read status `OK` metadata.
   * Attention: these methods do not throw errors when non-zero status codes are received.
   */
  $raw = {
    /**
     * Server streaming: /MatchGprcService/GetAllStream
     *
     * @param requestMessage Request message
     * @param requestMetadata Request metadata
     * @returns Observable<GrpcEvent<thisProto.MatchGrpcDto>>
     */
    getAllStream: (
      requestData: thisProto.Request,
      requestMetadata = new GrpcMetadata()
    ): Observable<GrpcEvent<thisProto.MatchGrpcDto>> => {
      return this.handler.handle({
        type: GrpcCallType.serverStream,
        client: this.client,
        path: '/MatchGprcService/GetAllStream',
        requestData,
        requestMetadata,
        requestClass: thisProto.Request,
        responseClass: thisProto.MatchGrpcDto
      });
    },
    /**
     * Unary call: /MatchGprcService/GetAll
     *
     * @param requestMessage Request message
     * @param requestMetadata Request metadata
     * @returns Observable<GrpcEvent<thisProto.Response>>
     */
    getAll: (
      requestData: thisProto.Request,
      requestMetadata = new GrpcMetadata()
    ): Observable<GrpcEvent<thisProto.Response>> => {
      return this.handler.handle({
        type: GrpcCallType.unary,
        client: this.client,
        path: '/MatchGprcService/GetAll',
        requestData,
        requestMetadata,
        requestClass: thisProto.Request,
        responseClass: thisProto.Response
      });
    },
    /**
     * Bidirectional streaming: /MatchGprcService/GetAllRequested
     *
     * @param requestMessage Request message
     * @param requestMetadata Request metadata
     * @returns Observable<GrpcEvent<thisProto.Response>>
     */
    getAllRequested: (
      requestData: Observable<thisProto.Request>,
      requestMetadata = new GrpcMetadata()
    ): Observable<GrpcEvent<thisProto.Response>> => {
      return this.handler.handle({
        type: GrpcCallType.bidiStream,
        client: this.client,
        path: '/MatchGprcService/GetAllRequested',
        requestData,
        requestMetadata,
        requestClass: thisProto.Request,
        responseClass: thisProto.Response
      });
    }
  };

  constructor(
    @Optional() @Inject(GRPC_MATCH_GPRC_SERVICE_CLIENT_SETTINGS) settings: any,
    @Inject(GRPC_CLIENT_FACTORY) clientFactory: GrpcClientFactory<any>,
    private handler: GrpcHandler
  ) {
    this.client = clientFactory.createClient('MatchGprcService', settings);
  }

  /**
   * Server streaming @/MatchGprcService/GetAllStream
   *
   * @param requestMessage Request message
   * @param requestMetadata Request metadata
   * @returns Observable<thisProto.MatchGrpcDto>
   */
  getAllStream(
    requestData: thisProto.Request,
    requestMetadata = new GrpcMetadata()
  ): Observable<thisProto.MatchGrpcDto> {
    return this.$raw
      .getAllStream(requestData, requestMetadata)
      .pipe(throwStatusErrors(), takeMessages());
  }

  /**
   * Unary call @/MatchGprcService/GetAll
   *
   * @param requestMessage Request message
   * @param requestMetadata Request metadata
   * @returns Observable<thisProto.Response>
   */
  getAll(
    requestData: thisProto.Request,
    requestMetadata = new GrpcMetadata()
  ): Observable<thisProto.Response> {
    return this.$raw
      .getAll(requestData, requestMetadata)
      .pipe(throwStatusErrors(), takeMessages());
  }

  /**
   * Bidirectional streaming @/MatchGprcService/GetAllRequested
   *
   * @param requestMessage Request message
   * @param requestMetadata Request metadata
   * @returns Observable<thisProto.Response>
   */
  getAllRequested(
    requestData: Observable<thisProto.Request>,
    requestMetadata = new GrpcMetadata()
  ): Observable<thisProto.Response> {
    return this.$raw
      .getAllRequested(requestData, requestMetadata)
      .pipe(throwStatusErrors(), takeMessages());
  }
}
