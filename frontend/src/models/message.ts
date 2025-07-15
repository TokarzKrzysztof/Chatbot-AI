export enum MessageReaction {
  None,
  Positive,
  Negative,
}

export type Message = {
  id: string;
  createdAt: string;
  text: string;
  isAnswer: boolean;
  reaction: MessageReaction;

  // UI
  isGenerating?: boolean;
};
