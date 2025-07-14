import { ChangeDetectionStrategy, Component, DestroyRef, inject } from '@angular/core';
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

  messages = new Query({
    loader: () => this.chatService.getChatHistory(),
  });

  async onSendSuccess(question: string) {
    this.messages.update((prev) => [...prev!, { text: question } as Message]);

    this.chatService
      .getChatResponse()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (result) => {
          const messages = this.messages.data()!;
          const last = messages[messages.length - 1];
          if (last.isAnswer) {
            last.text = result;
          } else {
            messages.push({ text: result, isAnswer: true } as Message);
          }

          this.messages.set([...messages]);
        },
        complete: () => {
          this.messages.refetch();
        },
      });
  }
}
