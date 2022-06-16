export * from './match.service';
import { MatchService } from './match.service';
export * from './matchQueue.service';
import { MatchQueueService } from './matchQueue.service';
export * from './player.service';
import { PlayerService } from './player.service';
export const APIS = [MatchService, MatchQueueService, PlayerService];
