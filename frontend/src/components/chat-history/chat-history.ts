import { NgClass } from '@angular/common';
import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { ChatService } from '../../services/api/chat.service';
import { Query } from '../../utils/query';

@Component({
  selector: 'app-chat-history',
  imports: [NgClass],
  templateUrl: './chat-history.html',
  styleUrl: './chat-history.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ChatHistory {
  private chatService = inject(ChatService);

  messages = new Query({
    loader: () => this.chatService.getChatHistory()
  })
}
