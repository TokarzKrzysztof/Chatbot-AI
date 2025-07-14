import { NgClass } from '@angular/common';
import { ChangeDetectionStrategy, Component, input } from '@angular/core';
import { Message } from '../../models/message';

@Component({
  selector: 'app-chat-history',
  imports: [NgClass],
  templateUrl: './chat-history.html',
  styleUrl: './chat-history.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ChatHistory {
  messages = input.required<Message[]>();
  generatedResponse = input.required<string | null>();
}
