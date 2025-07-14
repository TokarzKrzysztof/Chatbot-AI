import { ChangeDetectionStrategy, Component } from '@angular/core';
import { MessageField } from '../message-field/message-field';

@Component({
  selector: 'app-chatbot',
  imports: [MessageField],
  templateUrl: './chatbot.html',
  styleUrl: './chatbot.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class Chatbot {}
