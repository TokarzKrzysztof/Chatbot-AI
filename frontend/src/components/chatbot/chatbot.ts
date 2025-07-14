import { ChangeDetectionStrategy, Component, DestroyRef, inject, signal } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { Message } from '../../models/message';
import { ChatService } from '../../services/api/chat.service';
import { Query } from '../../utils/query';
import { ChatHistory } from '../chat-history/chat-history';
import { MessageField } from '../message-field/message-field';

@Component({
  selector: 'app-chatbot',
  imports: [MessageField, ChatHistory],
  templateUrl: './chatbot.html',
  styleUrl: './chatbot.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class Chatbot {
  private destroyRef = inject(DestroyRef);
  private chatService = inject(ChatService);

  generatedResponse = signal<string | null>(null);
  messages = new Query({
    loader: () => this.chatService.getChatHistory(),
  });

  onSendSuccess(question: string) {
    this.messages.update((prev) => [...prev!, { text: question } as Message]);
    this.chatService
      .getChatResponse()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (result) => {
          this.generatedResponse.set(result);
        },
        complete: () => {
          this.generatedResponse.set(null);
          this.messages.refetch();
        },
      });
  }
}
