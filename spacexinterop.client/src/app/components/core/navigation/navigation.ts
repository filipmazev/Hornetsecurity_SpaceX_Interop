import { NgClass } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatMenuModule } from '@angular/material/menu';
import { MatIconModule } from '@angular/material/icon';
import { FormsModule } from '@angular/forms';
import { RouterLink, RouterLinkActive } from "@angular/router";
import { Settings } from '../settings/settings';
import { DEFAULT_ANIMATION_DELAY } from '../../../shared/constants/animation.constants';
import { IScrollLockConfig } from '../../../shared/interfaces/services/scroll-lock-config.interface';
import { ScrollLockService } from '../../../shared/services/core/ui/scroll-lock.service';
import { INavLink } from '../../../shared/interfaces/ui/inav-link.interface';
import { AuthService } from '../../../shared/services/client/auth.service';
import { Subject, takeUntil } from 'rxjs';
import { UserResponse } from '../../../shared/classes/models/responses/UserResponse.model';
import { EMPTY_STRING } from '../../../shared/constants/common.constants';
import * as classNames from "../../../shared/constants/class-names.constants"

@Component({
  selector: 'navigation',
  standalone: true,
  imports: [
    MatMenuModule,
    MatIconModule,
    FormsModule,
    Settings,
    MatIconModule,
    RouterLink,
    RouterLinkActive,
    NgClass
  ],
  templateUrl: './navigation.html',
  styleUrl: './navigation.scss'
})
export class NavigationComponent implements OnInit, OnDestroy {
  protected readonly classNames = classNames;
  protected readonly navigationLinks: INavLink[] = [
    { label: "Home", route: "/" },
    { label: "Launches", route: "/launches" },
  ]

  protected isOpen: boolean = false;
  private _currentUser?: UserResponse;

  protected get currentUser(): UserResponse | undefined {
    return this._currentUser;
  }

  protected set currentUser(user: UserResponse | undefined) {
    this._currentUser = user;
    this.currentUserInitials = this.resolveCurrentUserInitials(user);
  }

  protected currentUserInitials?: string = undefined;

  private unsubscribe$ = new Subject<void>();

  private scrollLockConfig = {
    allow_touch_input_on: [],
    main_container: document.body,
    handle_extreme_overflow: false,
    animation_duration: DEFAULT_ANIMATION_DELAY,
    handle_touch_input: true,
    mobile_only_touch_prevention: true
  } as IScrollLockConfig;

  constructor(
    private scrollLockService: ScrollLockService,
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

  //#region UI Methods
  protected toggleMenu() {
    if (!this.isOpen) {
      this.scrollLockService.disableScroll(this.scrollLockConfig);
    } else {
      this.scrollLockService.enableScroll();
    }

    this.isOpen = !this.isOpen;
  }

  private resolveCurrentUserInitials(user: UserResponse | undefined): string {
    return user ? (user.firstName[0] + user.lastName[0]).toUpperCase() : EMPTY_STRING;
  }
  //#endregion
}