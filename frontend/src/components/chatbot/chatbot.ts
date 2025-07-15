import { ChangeDetectionStrategy, Component, DestroyRef, inject, signal } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { Message } from '../../models/message';
import { ChatService } from '../../services/api/chat.service';
import { Mutation } from '../../utils/mutation';
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

  showStopButton = signal(false);

  messages = new Query({
    loader: () => this.chatService.getChatHistory(),
  });
  cancelResponseGeneration = new Mutation({
    fn: () => this.chatService.cancelResponseGeneration(),
  });

  async onSendSuccess(question: string) {
    this.messages.update((prev) => [...prev!, { text: question } as Message]);

    this.showStopButton.set(true);
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
            messages.push({ text: result, isAnswer: true, isGenerating: true } as Message);
          }

          this.messages.set([...messages]);
        },
        complete: () => {
          this.messages.refetch();
          this.showStopButton.set(false);
        },
      });
  }
}
