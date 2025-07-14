import { HttpErrorResponse } from "@angular/common/http";
import { signal } from "@angular/core";
import { firstValueFrom, Observable } from "rxjs";
import { HttpErrorData } from "./types/http-error-data";

export type MutationParams<TPayload extends unknown[], TResult> = {
  fn: (...args: TPayload) => Observable<TResult>;
};
export class Mutation<TPayload extends unknown[], TResult> {
  inProgress = signal(false);
  error = signal<HttpErrorData | null>(null);

  constructor(private params: MutationParams<TPayload, TResult>) {}

  async mutate(...args: TPayload): Promise<TResult> {
    return new Promise((resolve, reject) => {
      this.inProgress.set(true);

      const obs = this.params.fn(...args);
      firstValueFrom(obs)
        .then((res) => {
          this.inProgress.set(false);
          this.error.set(null);
          resolve(res);
        })
        .catch((err: HttpErrorResponse) => {
          const errorData =
            err.error instanceof ProgressEvent ? null : (err.error as HttpErrorData);

          this.inProgress.set(false);
          this.error.set(errorData);
          reject(errorData);
        });
    });
  }
}