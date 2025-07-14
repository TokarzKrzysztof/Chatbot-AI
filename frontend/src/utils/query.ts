import {
  inject,
  InjectionToken,
  Injector,
  linkedSignal,
  runInInjectionContext,
  signal,
  WritableSignal,
} from '@angular/core';
import { toObservable, toSignal } from '@angular/core/rxjs-interop';
import { BehaviorSubject, firstValueFrom, Observable, of, skip, switchMap, tap } from 'rxjs';

const toWritableSignal = <T>(obs: Observable<T>) => {
  const signal = toSignal(obs);
  return linkedSignal(() => signal());
};

const CACHE = new InjectionToken('cached data', {
  providedIn: 'root',
  factory: () => ({}) as Record<string, unknown>,
});

export type QueryParams<T> = { loader: () => Observable<T>; cachedWithKey?: string };
export class Query<T> {
  private injector = inject(Injector);
  private cache = inject(CACHE);

  private refetch$ = new BehaviorSubject<{ force: boolean }>({ force: false });
  isLoading = signal(false);
  data: WritableSignal<T | undefined>;

  constructor(private params: QueryParams<T>) {
    this.data = toWritableSignal(
      this.refetch$.pipe(
        tap(() => this.isLoading.set(true)),
        switchMap(({ force }) => {
          if (params.cachedWithKey) {
            if (this.cache[params.cachedWithKey] && !force) {
              return of(this.cache[params.cachedWithKey] as T);
            } else {
              return runInInjectionContext(this.injector, params.loader).pipe(
                tap((data) => {
                  if (params.cachedWithKey) {
                    this.cache[params.cachedWithKey] = data;
                  }
                }),
              );
            }
          }
          return runInInjectionContext(this.injector, params.loader);
        }),
        tap(() => this.isLoading.set(false)),
      ),
    );
  }

  refetch() {
    this.refetch$.next({ force: true });
    return runInInjectionContext(this.injector, () => {
      return firstValueFrom(toObservable(this.data).pipe(skip(1)));
    });
  }

  set(data: T) {
    this.data.set(data);
    if (this.params.cachedWithKey) {
      this.cache[this.params.cachedWithKey] = this.data();
    }
  }

  update(cb: (prev: T | undefined) => T) {
    this.data.update(cb);
    if (this.params.cachedWithKey) {
      this.cache[this.params.cachedWithKey] = this.data();
    }
  }
}
