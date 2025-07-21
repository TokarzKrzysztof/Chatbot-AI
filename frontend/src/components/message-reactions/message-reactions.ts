import { NgClass } from '@angular/common';
import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  inject,
  input,
} from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { Message, MessageReaction } from '../../models/message';
import { SetMessageReactionData } from '../../models/resources/set-message-reaction-data';
import { ChatService } from '../../services/api/chat.service';
import { Mutation } from '../../utils/mutation';

@Component({
  selector: 'app-message-reactions',
  imports: [MatButtonModule, MatIconModule, NgClass],
  templateUrl: './message-reactions.html',
  styleUrl: './message-reactions.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MessageReactions {
  private cdr = inject(ChangeDetectorRef);
  private chatService = inject(ChatService);

  readonly MessageReaction = MessageReaction;

  message = input.required<Message>();

  setMessageReaction = new Mutation({
    fn: (data: SetMessageReactionData) => this.chatService.setMessageReaction(data),
  });

ngOnInit() {
  console.log(this.message().reaction)
}

  setReaction(value: MessageReaction) {
    let newReaction: MessageReaction | undefined = undefined;
    if(this.message().reaction === value) {
      newReaction = MessageReaction.None
    } else {
      newReaction = value;
    }
    this.setMessageReaction.mutate({ messageId: this.message().id, reaction: newReaction }).then(() => {
      this.message().reaction = newReaction;
      this.cdr.markForCheck();
    });
  }
}
