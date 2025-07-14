import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environement/environment';
import { Message } from '../../models/message';

const url = `${environment.apiUrl}/Chat`;

@Injectable({
  providedIn: 'root',
})
export class ChatService {
  private http = inject(HttpClient);

  getChatHistory() {
    return this.http.get<Message[]>(`${url}/GetChatHistory`);
  }

  sendMessage(data: Pick<Message, 'text'>) {
    return this.http.post<void>(`${url}/SendMessage`, data);
  }
}
