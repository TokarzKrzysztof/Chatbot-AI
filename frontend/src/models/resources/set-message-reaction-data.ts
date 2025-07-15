import { Message } from "../message";

export type SetMessageReactionData = {
    messageId: Message['id'];
    reaction: Message['reaction']
}