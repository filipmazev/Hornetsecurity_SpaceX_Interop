import { Component, OnInit } from '@angular/core';
import { GenericButton } from '../../core/generic-button/generic-button';
import { Router } from '@angular/router';
import { AuthService } from '../../../shared/services/client/auth.service';
import { UserResponse } from '../../../shared/classes/models/responses/UserResponse.model';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-landing',
  imports: [
    GenericButton
  ],
  templateUrl: './landing.html',
  styleUrl: './landing.scss'
})
export class Landing implements OnInit {
  protected currentUser?: UserResponse;
  private unsubscribe$ = new Subject<void>();

  constructor(
    private router: Router,
    private authService: AuthService
  ) {
    this.currentUser = this.authService.currentUser;
  }

  public ngOnInit(): void {
    this.createSubscriptions();
  }

  public ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  private createSubscriptions(): void {
    this.authService.currentUser$().pipe(takeUntil(this.unsubscribe$)).subscribe(result => {
      this.currentUser = result;
    });
  }

  protected navigateToLaunches(): void {
    this.router.navigate(['/launches']);
  }
}
