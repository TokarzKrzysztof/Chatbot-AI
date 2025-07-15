import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environement/environment';
import { Message } from '../../models/message';
import { SetMessageReactionData } from '../../models/resources/set-message-reaction-data';

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

  setMessageReaction(data: SetMessageReactionData) {
    return this.http.patch<void>(`${url}/SetMessageReaction`, data);
  }

  cancelResponseGeneration() {
    return this.http.delete<void>(`${url}/CancelResponseGeneration`);
  }

  getChatResponse(): Observable<string> {
    return new Observable((observer) => {
      const eventSource = new EventSource(`${url}/GetChatResponse`);

      eventSource.onmessage = (event) => {
        observer.next(JSON.parse(event.data));
      };

      eventSource.onerror = () => {
        // error means that connection was closed for some reason, but also when no data is available
        observer.complete();
        eventSource.close();
      };

      return () => {
        eventSource.close();
      };
    });
  }
}
