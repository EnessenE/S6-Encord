import { Message } from './message';

export class TextChannel {
    id: string;
    name: string;
    type: string;
    createdDate: Date;
    guildID: string;
    messages: Message[]
}
