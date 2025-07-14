import { ChangeDetectionStrategy, Component } from '@angular/core';
import { ChatHistory } from "../chat-history/chat-history";
import { MessageField } from '../message-field/message-field';

@Component({
  selector: 'app-chatbot',
  imports: [MessageField, ChatHistory],
  templateUrl: './chatbot.html',
  styleUrl: './chatbot.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class Chatbot {}
