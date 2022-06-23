/* tslint:disable */
/* eslint-disable */
// @ts-nocheck
//
// THIS IS A GENERATED FILE
// DO NOT MODIFY IT! YOUR CHANGES WILL BE LOST
import {
  GrpcMessage,
  RecursivePartial,
  ToProtobufJSONOptions
} from '@ngx-grpc/common';
import { BinaryReader, BinaryWriter, ByteSource } from 'google-protobuf';
import * as googleProtobuf000 from '@ngx-grpc/well-known-types';
/**
 * Message implementation for Request
 */
export class Request implements GrpcMessage {
  static id = 'Request';

  /**
   * Deserialize binary data to message
   * @param instance message instance
   */
  static deserializeBinary(bytes: ByteSource) {
    const instance = new Request();
    Request.deserializeBinaryFromReader(instance, new BinaryReader(bytes));
    return instance;
  }

  /**
   * Check all the properties and set default protobuf values if necessary
   * @param _instance message instance
   */
  static refineValues(_instance: Request) {}

  /**
   * Deserializes / reads binary message into message instance using provided binary reader
   * @param _instance message instance
   * @param _reader binary reader instance
   */
  static deserializeBinaryFromReader(
    _instance: Request,
    _reader: BinaryReader
  ) {
    while (_reader.nextField()) {
      if (_reader.isEndGroup()) break;

      switch (_reader.getFieldNumber()) {
        default:
          _reader.skipField();
      }
    }

    Request.refineValues(_instance);
  }

  /**
   * Serializes a message to binary format using provided binary reader
   * @param _instance message instance
   * @param _writer binary writer instance
   */
  static serializeBinaryToWriter(_instance: Request, _writer: BinaryWriter) {}

  /**
   * Message constructor. Initializes the properties and applies default Protobuf values if necessary
   * @param _value initial values object or instance of Request to deeply clone from
   */
  constructor(_value?: RecursivePartial<Request.AsObject>) {
    _value = _value || {};
    Request.refineValues(this);
  }

  /**
   * Serialize message to binary data
   * @param instance message instance
   */
  serializeBinary() {
    const writer = new BinaryWriter();
    Request.serializeBinaryToWriter(this, writer);
    return writer.getResultBuffer();
  }

  /**
   * Cast message to standard JavaScript object (all non-primitive values are deeply cloned)
   */
  toObject(): Request.AsObject {
    return {};
  }

  /**
   * Convenience method to support JSON.stringify(message), replicates the structure of toObject()
   */
  toJSON() {
    return this.toObject();
  }

  /**
   * Cast message to JSON using protobuf JSON notation: https://developers.google.com/protocol-buffers/docs/proto3#json
   * Attention: output differs from toObject() e.g. enums are represented as names and not as numbers, Timestamp is an ISO Date string format etc.
   * If the message itself or some of descendant messages is google.protobuf.Any, you MUST provide a message pool as options. If not, the messagePool is not required
   */
  toProtobufJSON(
    // @ts-ignore
    options?: ToProtobufJSONOptions
  ): Request.AsProtobufJSON {
    return {};
  }
}
export module Request {
  /**
   * Standard JavaScript object representation for Request
   */
  export interface AsObject {}

  /**
   * Protobuf JSON representation for Request
   */
  export interface AsProtobufJSON {}
}

/**
 * Message implementation for Response
 */
export class Response implements GrpcMessage {
  static id = 'Response';

  /**
   * Deserialize binary data to message
   * @param instance message instance
   */
  static deserializeBinary(bytes: ByteSource) {
    const instance = new Response();
    Response.deserializeBinaryFromReader(instance, new BinaryReader(bytes));
    return instance;
  }

  /**
   * Check all the properties and set default protobuf values if necessary
   * @param _instance message instance
   */
  static refineValues(_instance: Response) {
    _instance.result = _instance.result || [];
  }

  /**
   * Deserializes / reads binary message into message instance using provided binary reader
   * @param _instance message instance
   * @param _reader binary reader instance
   */
  static deserializeBinaryFromReader(
    _instance: Response,
    _reader: BinaryReader
  ) {
    while (_reader.nextField()) {
      if (_reader.isEndGroup()) break;

      switch (_reader.getFieldNumber()) {
        case 1:
          const messageInitializer1 = new MatchGrpcDto();
          _reader.readMessage(
            messageInitializer1,
            MatchGrpcDto.deserializeBinaryFromReader
          );
          (_instance.result = _instance.result || []).push(messageInitializer1);
          break;
        default:
          _reader.skipField();
      }
    }

    Response.refineValues(_instance);
  }

  /**
   * Serializes a message to binary format using provided binary reader
   * @param _instance message instance
   * @param _writer binary writer instance
   */
  static serializeBinaryToWriter(_instance: Response, _writer: BinaryWriter) {
    if (_instance.result && _instance.result.length) {
      _writer.writeRepeatedMessage(
        1,
        _instance.result as any,
        MatchGrpcDto.serializeBinaryToWriter
      );
    }
  }

  private _result?: MatchGrpcDto[];

  /**
   * Message constructor. Initializes the properties and applies default Protobuf values if necessary
   * @param _value initial values object or instance of Response to deeply clone from
   */
  constructor(_value?: RecursivePartial<Response.AsObject>) {
    _value = _value || {};
    this.result = (_value.result || []).map(m => new MatchGrpcDto(m));
    Response.refineValues(this);
  }
  get result(): MatchGrpcDto[] | undefined {
    return this._result;
  }
  set result(value: MatchGrpcDto[] | undefined) {
    this._result = value;
  }

  /**
   * Serialize message to binary data
   * @param instance message instance
   */
  serializeBinary() {
    const writer = new BinaryWriter();
    Response.serializeBinaryToWriter(this, writer);
    return writer.getResultBuffer();
  }

  /**
   * Cast message to standard JavaScript object (all non-primitive values are deeply cloned)
   */
  toObject(): Response.AsObject {
    return {
      result: (this.result || []).map(m => m.toObject())
    };
  }

  /**
   * Convenience method to support JSON.stringify(message), replicates the structure of toObject()
   */
  toJSON() {
    return this.toObject();
  }

  /**
   * Cast message to JSON using protobuf JSON notation: https://developers.google.com/protocol-buffers/docs/proto3#json
   * Attention: output differs from toObject() e.g. enums are represented as names and not as numbers, Timestamp is an ISO Date string format etc.
   * If the message itself or some of descendant messages is google.protobuf.Any, you MUST provide a message pool as options. If not, the messagePool is not required
   */
  toProtobufJSON(
    // @ts-ignore
    options?: ToProtobufJSONOptions
  ): Response.AsProtobufJSON {
    return {
      result: (this.result || []).map(m => m.toProtobufJSON(options))
    };
  }
}
export module Response {
  /**
   * Standard JavaScript object representation for Response
   */
  export interface AsObject {
    result?: MatchGrpcDto.AsObject[];
  }

  /**
   * Protobuf JSON representation for Response
   */
  export interface AsProtobufJSON {
    result?: MatchGrpcDto.AsProtobufJSON[] | null;
  }
}

/**
 * Message implementation for MatchGrpcDto
 */
export class MatchGrpcDto implements GrpcMessage {
  static id = 'MatchGrpcDto';

  /**
   * Deserialize binary data to message
   * @param instance message instance
   */
  static deserializeBinary(bytes: ByteSource) {
    const instance = new MatchGrpcDto();
    MatchGrpcDto.deserializeBinaryFromReader(instance, new BinaryReader(bytes));
    return instance;
  }

  /**
   * Check all the properties and set default protobuf values if necessary
   * @param _instance message instance
   */
  static refineValues(_instance: MatchGrpcDto) {
    _instance.id = _instance.id || 0;
    _instance.participationA = _instance.participationA || undefined;
    _instance.participationB = _instance.participationB || undefined;
    _instance.registrationDate = _instance.registrationDate || undefined;
    _instance.playDate = _instance.playDate || undefined;
  }

  /**
   * Deserializes / reads binary message into message instance using provided binary reader
   * @param _instance message instance
   * @param _reader binary reader instance
   */
  static deserializeBinaryFromReader(
    _instance: MatchGrpcDto,
    _reader: BinaryReader
  ) {
    while (_reader.nextField()) {
      if (_reader.isEndGroup()) break;

      switch (_reader.getFieldNumber()) {
        case 1:
          _instance.id = _reader.readInt32();
          break;
        case 2:
          _instance.participationA = new ParticipationGrpcDto();
          _reader.readMessage(
            _instance.participationA,
            ParticipationGrpcDto.deserializeBinaryFromReader
          );
          break;
        case 3:
          _instance.participationB = new ParticipationGrpcDto();
          _reader.readMessage(
            _instance.participationB,
            ParticipationGrpcDto.deserializeBinaryFromReader
          );
          break;
        case 4:
          _instance.registrationDate = new googleProtobuf000.Timestamp();
          _reader.readMessage(
            _instance.registrationDate,
            googleProtobuf000.Timestamp.deserializeBinaryFromReader
          );
          break;
        case 5:
          _instance.playDate = new googleProtobuf000.Timestamp();
          _reader.readMessage(
            _instance.playDate,
            googleProtobuf000.Timestamp.deserializeBinaryFromReader
          );
          break;
        default:
          _reader.skipField();
      }
    }

    MatchGrpcDto.refineValues(_instance);
  }

  /**
   * Serializes a message to binary format using provided binary reader
   * @param _instance message instance
   * @param _writer binary writer instance
   */
  static serializeBinaryToWriter(
    _instance: MatchGrpcDto,
    _writer: BinaryWriter
  ) {
    if (_instance.id) {
      _writer.writeInt32(1, _instance.id);
    }
    if (_instance.participationA) {
      _writer.writeMessage(
        2,
        _instance.participationA as any,
        ParticipationGrpcDto.serializeBinaryToWriter
      );
    }
    if (_instance.participationB) {
      _writer.writeMessage(
        3,
        _instance.participationB as any,
        ParticipationGrpcDto.serializeBinaryToWriter
      );
    }
    if (_instance.registrationDate) {
      _writer.writeMessage(
        4,
        _instance.registrationDate as any,
        googleProtobuf000.Timestamp.serializeBinaryToWriter
      );
    }
    if (_instance.playDate) {
      _writer.writeMessage(
        5,
        _instance.playDate as any,
        googleProtobuf000.Timestamp.serializeBinaryToWriter
      );
    }
  }

  private _id?: number;
  private _participationA?: ParticipationGrpcDto;
  private _participationB?: ParticipationGrpcDto;
  private _registrationDate?: googleProtobuf000.Timestamp;
  private _playDate?: googleProtobuf000.Timestamp;

  /**
   * Message constructor. Initializes the properties and applies default Protobuf values if necessary
   * @param _value initial values object or instance of MatchGrpcDto to deeply clone from
   */
  constructor(_value?: RecursivePartial<MatchGrpcDto.AsObject>) {
    _value = _value || {};
    this.id = _value.id;
    this.participationA = _value.participationA
      ? new ParticipationGrpcDto(_value.participationA)
      : undefined;
    this.participationB = _value.participationB
      ? new ParticipationGrpcDto(_value.participationB)
      : undefined;
    this.registrationDate = _value.registrationDate
      ? new googleProtobuf000.Timestamp(_value.registrationDate)
      : undefined;
    this.playDate = _value.playDate
      ? new googleProtobuf000.Timestamp(_value.playDate)
      : undefined;
    MatchGrpcDto.refineValues(this);
  }
  get id(): number | undefined {
    return this._id;
  }
  set id(value: number | undefined) {
    this._id = value;
  }
  get participationA(): ParticipationGrpcDto | undefined {
    return this._participationA;
  }
  set participationA(value: ParticipationGrpcDto | undefined) {
    this._participationA = value;
  }
  get participationB(): ParticipationGrpcDto | undefined {
    return this._participationB;
  }
  set participationB(value: ParticipationGrpcDto | undefined) {
    this._participationB = value;
  }
  get registrationDate(): googleProtobuf000.Timestamp | undefined {
    return this._registrationDate;
  }
  set registrationDate(value: googleProtobuf000.Timestamp | undefined) {
    this._registrationDate = value;
  }
  get playDate(): googleProtobuf000.Timestamp | undefined {
    return this._playDate;
  }
  set playDate(value: googleProtobuf000.Timestamp | undefined) {
    this._playDate = value;
  }

  /**
   * Serialize message to binary data
   * @param instance message instance
   */
  serializeBinary() {
    const writer = new BinaryWriter();
    MatchGrpcDto.serializeBinaryToWriter(this, writer);
    return writer.getResultBuffer();
  }

  /**
   * Cast message to standard JavaScript object (all non-primitive values are deeply cloned)
   */
  toObject(): MatchGrpcDto.AsObject {
    return {
      id: this.id,
      participationA: this.participationA
        ? this.participationA.toObject()
        : undefined,
      participationB: this.participationB
        ? this.participationB.toObject()
        : undefined,
      registrationDate: this.registrationDate
        ? this.registrationDate.toObject()
        : undefined,
      playDate: this.playDate ? this.playDate.toObject() : undefined
    };
  }

  /**
   * Convenience method to support JSON.stringify(message), replicates the structure of toObject()
   */
  toJSON() {
    return this.toObject();
  }

  /**
   * Cast message to JSON using protobuf JSON notation: https://developers.google.com/protocol-buffers/docs/proto3#json
   * Attention: output differs from toObject() e.g. enums are represented as names and not as numbers, Timestamp is an ISO Date string format etc.
   * If the message itself or some of descendant messages is google.protobuf.Any, you MUST provide a message pool as options. If not, the messagePool is not required
   */
  toProtobufJSON(
    // @ts-ignore
    options?: ToProtobufJSONOptions
  ): MatchGrpcDto.AsProtobufJSON {
    return {
      id: this.id,
      participationA: this.participationA
        ? this.participationA.toProtobufJSON(options)
        : null,
      participationB: this.participationB
        ? this.participationB.toProtobufJSON(options)
        : null,
      registrationDate: this.registrationDate
        ? this.registrationDate.toProtobufJSON(options)
        : null,
      playDate: this.playDate ? this.playDate.toProtobufJSON(options) : null
    };
  }
}
export module MatchGrpcDto {
  /**
   * Standard JavaScript object representation for MatchGrpcDto
   */
  export interface AsObject {
    id?: number;
    participationA?: ParticipationGrpcDto.AsObject;
    participationB?: ParticipationGrpcDto.AsObject;
    registrationDate?: googleProtobuf000.Timestamp.AsObject;
    playDate?: googleProtobuf000.Timestamp.AsObject;
  }

  /**
   * Protobuf JSON representation for MatchGrpcDto
   */
  export interface AsProtobufJSON {
    id?: number;
    participationA?: ParticipationGrpcDto.AsProtobufJSON | null;
    participationB?: ParticipationGrpcDto.AsProtobufJSON | null;
    registrationDate?: googleProtobuf000.Timestamp.AsProtobufJSON | null;
    playDate?: googleProtobuf000.Timestamp.AsProtobufJSON | null;
  }
}

/**
 * Message implementation for ParticipationGrpcDto
 */
export class ParticipationGrpcDto implements GrpcMessage {
  static id = 'ParticipationGrpcDto';

  /**
   * Deserialize binary data to message
   * @param instance message instance
   */
  static deserializeBinary(bytes: ByteSource) {
    const instance = new ParticipationGrpcDto();
    ParticipationGrpcDto.deserializeBinaryFromReader(
      instance,
      new BinaryReader(bytes)
    );
    return instance;
  }

  /**
   * Check all the properties and set default protobuf values if necessary
   * @param _instance message instance
   */
  static refineValues(_instance: ParticipationGrpcDto) {
    _instance.id = _instance.id || 0;
    _instance.player = _instance.player || undefined;
    _instance.startingRank = _instance.startingRank || 0;
    _instance.finishingRank = _instance.finishingRank || 0;
    _instance.rankDifference = _instance.rankDifference || 0;
  }

  /**
   * Deserializes / reads binary message into message instance using provided binary reader
   * @param _instance message instance
   * @param _reader binary reader instance
   */
  static deserializeBinaryFromReader(
    _instance: ParticipationGrpcDto,
    _reader: BinaryReader
  ) {
    while (_reader.nextField()) {
      if (_reader.isEndGroup()) break;

      switch (_reader.getFieldNumber()) {
        case 1:
          _instance.id = _reader.readInt32();
          break;
        case 2:
          _instance.player = new PlayerGrpcDto();
          _reader.readMessage(
            _instance.player,
            PlayerGrpcDto.deserializeBinaryFromReader
          );
          break;
        case 3:
          _instance.startingRank = _reader.readInt32();
          break;
        case 4:
          _instance.finishingRank = _reader.readInt32();
          break;
        case 5:
          _instance.rankDifference = _reader.readInt32();
          break;
        case 6:
          _instance.hasWon = _reader.readBool();
          break;
        default:
          _reader.skipField();
      }
    }

    ParticipationGrpcDto.refineValues(_instance);
  }

  /**
   * Serializes a message to binary format using provided binary reader
   * @param _instance message instance
   * @param _writer binary writer instance
   */
  static serializeBinaryToWriter(
    _instance: ParticipationGrpcDto,
    _writer: BinaryWriter
  ) {
    if (_instance.id) {
      _writer.writeInt32(1, _instance.id);
    }
    if (_instance.player) {
      _writer.writeMessage(
        2,
        _instance.player as any,
        PlayerGrpcDto.serializeBinaryToWriter
      );
    }
    if (_instance.startingRank) {
      _writer.writeInt32(3, _instance.startingRank);
    }
    if (_instance.finishingRank) {
      _writer.writeInt32(4, _instance.finishingRank);
    }
    if (_instance.rankDifference) {
      _writer.writeInt32(5, _instance.rankDifference);
    }
    if (_instance.hasWon !== undefined && _instance.hasWon !== null) {
      _writer.writeBool(6, _instance.hasWon);
    }
  }

  private _id?: number;
  private _player?: PlayerGrpcDto;
  private _startingRank?: number;
  private _finishingRank?: number;
  private _rankDifference?: number;
  private _hasWon?: boolean;

  /**
   * Message constructor. Initializes the properties and applies default Protobuf values if necessary
   * @param _value initial values object or instance of ParticipationGrpcDto to deeply clone from
   */
  constructor(_value?: RecursivePartial<ParticipationGrpcDto.AsObject>) {
    _value = _value || {};
    this.id = _value.id;
    this.player = _value.player ? new PlayerGrpcDto(_value.player) : undefined;
    this.startingRank = _value.startingRank;
    this.finishingRank = _value.finishingRank;
    this.rankDifference = _value.rankDifference;
    this.hasWon = _value.hasWon;
    ParticipationGrpcDto.refineValues(this);
  }
  get id(): number | undefined {
    return this._id;
  }
  set id(value: number | undefined) {
    this._id = value;
  }
  get player(): PlayerGrpcDto | undefined {
    return this._player;
  }
  set player(value: PlayerGrpcDto | undefined) {
    this._player = value;
  }
  get startingRank(): number | undefined {
    return this._startingRank;
  }
  set startingRank(value: number | undefined) {
    this._startingRank = value;
  }
  get finishingRank(): number | undefined {
    return this._finishingRank;
  }
  set finishingRank(value: number | undefined) {
    this._finishingRank = value;
  }
  get rankDifference(): number | undefined {
    return this._rankDifference;
  }
  set rankDifference(value: number | undefined) {
    this._rankDifference = value;
  }
  get hasWon(): boolean | undefined {
    return this._hasWon;
  }
  set hasWon(value: boolean | undefined) {
    this._hasWon = value;
  }

  /**
   * Serialize message to binary data
   * @param instance message instance
   */
  serializeBinary() {
    const writer = new BinaryWriter();
    ParticipationGrpcDto.serializeBinaryToWriter(this, writer);
    return writer.getResultBuffer();
  }

  /**
   * Cast message to standard JavaScript object (all non-primitive values are deeply cloned)
   */
  toObject(): ParticipationGrpcDto.AsObject {
    return {
      id: this.id,
      player: this.player ? this.player.toObject() : undefined,
      startingRank: this.startingRank,
      finishingRank: this.finishingRank,
      rankDifference: this.rankDifference,
      hasWon: this.hasWon
    };
  }

  /**
   * Convenience method to support JSON.stringify(message), replicates the structure of toObject()
   */
  toJSON() {
    return this.toObject();
  }

  /**
   * Cast message to JSON using protobuf JSON notation: https://developers.google.com/protocol-buffers/docs/proto3#json
   * Attention: output differs from toObject() e.g. enums are represented as names and not as numbers, Timestamp is an ISO Date string format etc.
   * If the message itself or some of descendant messages is google.protobuf.Any, you MUST provide a message pool as options. If not, the messagePool is not required
   */
  toProtobufJSON(
    // @ts-ignore
    options?: ToProtobufJSONOptions
  ): ParticipationGrpcDto.AsProtobufJSON {
    return {
      id: this.id,
      player: this.player ? this.player.toProtobufJSON(options) : null,
      startingRank: this.startingRank,
      finishingRank: this.finishingRank,
      rankDifference: this.rankDifference,
      hasWon: this.hasWon === undefined ? null : this.hasWon
    };
  }
}
export module ParticipationGrpcDto {
  /**
   * Standard JavaScript object representation for ParticipationGrpcDto
   */
  export interface AsObject {
    id?: number;
    player?: PlayerGrpcDto.AsObject;
    startingRank?: number;
    finishingRank?: number;
    rankDifference?: number;
    hasWon?: boolean;
  }

  /**
   * Protobuf JSON representation for ParticipationGrpcDto
   */
  export interface AsProtobufJSON {
    id?: number;
    player?: PlayerGrpcDto.AsProtobufJSON | null;
    startingRank?: number;
    finishingRank?: number;
    rankDifference?: number;
    hasWon?: boolean | null;
  }
}

/**
 * Message implementation for PlayerGrpcDto
 */
export class PlayerGrpcDto implements GrpcMessage {
  static id = 'PlayerGrpcDto';

  /**
   * Deserialize binary data to message
   * @param instance message instance
   */
  static deserializeBinary(bytes: ByteSource) {
    const instance = new PlayerGrpcDto();
    PlayerGrpcDto.deserializeBinaryFromReader(
      instance,
      new BinaryReader(bytes)
    );
    return instance;
  }

  /**
   * Check all the properties and set default protobuf values if necessary
   * @param _instance message instance
   */
  static refineValues(_instance: PlayerGrpcDto) {
    _instance.id = _instance.id || 0;
    _instance.nickname = _instance.nickname || '';
    _instance.rank = _instance.rank || 0;
  }

  /**
   * Deserializes / reads binary message into message instance using provided binary reader
   * @param _instance message instance
   * @param _reader binary reader instance
   */
  static deserializeBinaryFromReader(
    _instance: PlayerGrpcDto,
    _reader: BinaryReader
  ) {
    while (_reader.nextField()) {
      if (_reader.isEndGroup()) break;

      switch (_reader.getFieldNumber()) {
        case 1:
          _instance.id = _reader.readInt32();
          break;
        case 2:
          _instance.nickname = _reader.readString();
          break;
        case 3:
          _instance.rank = _reader.readInt32();
          break;
        default:
          _reader.skipField();
      }
    }

    PlayerGrpcDto.refineValues(_instance);
  }

  /**
   * Serializes a message to binary format using provided binary reader
   * @param _instance message instance
   * @param _writer binary writer instance
   */
  static serializeBinaryToWriter(
    _instance: PlayerGrpcDto,
    _writer: BinaryWriter
  ) {
    if (_instance.id) {
      _writer.writeInt32(1, _instance.id);
    }
    if (_instance.nickname) {
      _writer.writeString(2, _instance.nickname);
    }
    if (_instance.rank) {
      _writer.writeInt32(3, _instance.rank);
    }
  }

  private _id?: number;
  private _nickname?: string;
  private _rank?: number;

  /**
   * Message constructor. Initializes the properties and applies default Protobuf values if necessary
   * @param _value initial values object or instance of PlayerGrpcDto to deeply clone from
   */
  constructor(_value?: RecursivePartial<PlayerGrpcDto.AsObject>) {
    _value = _value || {};
    this.id = _value.id;
    this.nickname = _value.nickname;
    this.rank = _value.rank;
    PlayerGrpcDto.refineValues(this);
  }
  get id(): number | undefined {
    return this._id;
  }
  set id(value: number | undefined) {
    this._id = value;
  }
  get nickname(): string | undefined {
    return this._nickname;
  }
  set nickname(value: string | undefined) {
    this._nickname = value;
  }
  get rank(): number | undefined {
    return this._rank;
  }
  set rank(value: number | undefined) {
    this._rank = value;
  }

  /**
   * Serialize message to binary data
   * @param instance message instance
   */
  serializeBinary() {
    const writer = new BinaryWriter();
    PlayerGrpcDto.serializeBinaryToWriter(this, writer);
    return writer.getResultBuffer();
  }

  /**
   * Cast message to standard JavaScript object (all non-primitive values are deeply cloned)
   */
  toObject(): PlayerGrpcDto.AsObject {
    return {
      id: this.id,
      nickname: this.nickname,
      rank: this.rank
    };
  }

  /**
   * Convenience method to support JSON.stringify(message), replicates the structure of toObject()
   */
  toJSON() {
    return this.toObject();
  }

  /**
   * Cast message to JSON using protobuf JSON notation: https://developers.google.com/protocol-buffers/docs/proto3#json
   * Attention: output differs from toObject() e.g. enums are represented as names and not as numbers, Timestamp is an ISO Date string format etc.
   * If the message itself or some of descendant messages is google.protobuf.Any, you MUST provide a message pool as options. If not, the messagePool is not required
   */
  toProtobufJSON(
    // @ts-ignore
    options?: ToProtobufJSONOptions
  ): PlayerGrpcDto.AsProtobufJSON {
    return {
      id: this.id,
      nickname: this.nickname,
      rank: this.rank
    };
  }
}
export module PlayerGrpcDto {
  /**
   * Standard JavaScript object representation for PlayerGrpcDto
   */
  export interface AsObject {
    id?: number;
    nickname?: string;
    rank?: number;
  }

  /**
   * Protobuf JSON representation for PlayerGrpcDto
   */
  export interface AsProtobufJSON {
    id?: number;
    nickname?: string;
    rank?: number;
  }
}
