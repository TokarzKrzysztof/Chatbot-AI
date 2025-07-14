import { TextFieldModule } from '@angular/cdk/text-field';
import { ChangeDetectionStrategy, Component, inject, model, output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { ChatService } from '../../services/api/chat.service';
import { Mutation } from '../../utils/mutation';

@Component({
  selector: 'app-message-field',
  imports: [
    MatFormFieldModule,
    MatInputModule,
    TextFieldModule,
    MatButtonModule,
    MatIconModule,
    FormsModule,
  ],
  templateUrl: './message-field.html',
  styleUrl: './message-field.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MessageField {
  private chatService = inject(ChatService);

  protected text = model('');
  onSendSuccess = output<string>();

  sendMessage = new Mutation({
    fn: (text: string) => this.chatService.sendMessage({ text }),
  });

  send() {
    this.sendMessage.mutate(this.text()).then(() => {
      this.onSendSuccess.emit(this.text());
      this.text.set('');
    });
  }
}
