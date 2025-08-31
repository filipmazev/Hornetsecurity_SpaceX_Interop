import { Component, OnDestroy, OnInit, signal } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { NavigationComponent } from './components/core/navigation/navigation';
import { AuthService } from './shared/services/client/auth.service';
import { Subject, takeUntil } from 'rxjs';
import { NgClass } from '@angular/common';
import { Spinner } from './components/core/spinner/spinner';

@Component({
  selector: 'app-root',
  imports: [
    RouterOutlet,
    Spinner,
    NavigationComponent,
    NgClass
],
  templateUrl: './app.html'
})
export class App implements OnInit, OnDestroy {
  protected readonly title = signal('spacexinterop.client');

  protected hasReceivedAuthStatus: boolean = false;
  protected isAuthenticated: boolean = false;

  private unsubscribe$ = new Subject<void>();

  constructor(
    private authService: AuthService,
    private router: Router
  ) { }

  public ngOnInit(): void {
    this.createSubscriptions();
  }

  public ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  private createSubscriptions(): void {
    this.authService.currentUser$()
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe(result => {
        this.hasReceivedAuthStatus = true;
        this.isAuthenticated = !!result;

        const currentUrl = this.router.url;

        if (currentUrl.startsWith('/register') || currentUrl.startsWith('/login')){
          if(result) this.router.navigate(['/']);
          else return;
        }

        if (!result) {
          this.router.navigate(['/login']);
        }
      });
  }

}