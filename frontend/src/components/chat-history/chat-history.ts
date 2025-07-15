import { NgClass } from '@angular/common';
import { ChangeDetectionStrategy, Component, input } from '@angular/core';
import { Message } from '../../models/message';
import { MessageReactions } from "../message-reactions/message-reactions";

@Component({
  selector: 'app-chat-history',
  imports: [NgClass, MessageReactions],
  templateUrl: './chat-history.html',
  styleUrl: './chat-history.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ChatHistory {
  messages = input.required<Message[]>();
}
