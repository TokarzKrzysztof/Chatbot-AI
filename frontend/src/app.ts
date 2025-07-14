import { Component } from '@angular/core';
import { Chatbot } from './components/chatbot/chatbot';

@Component({
  selector: 'app-root',
  template: `<app-chatbot></app-chatbot>`,
  imports: [Chatbot],
})
export class App {}
