import { NgClass } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatMenuModule } from '@angular/material/menu';
import { MatIconModule } from '@angular/material/icon';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink, RouterLinkActive } from "@angular/router";
import { Settings } from '../settings/settings';
import { DEFAULT_ANIMATION_DELAY } from '../../../shared/constants/animation.constants';
import { IScrollLockConfig } from '../../../shared/interfaces/services/scroll-lock-config.interface';
import { ScrollLockService } from '../../../shared/services/core/ui/scroll-lock.service';
import { INavLink } from '../../../shared/interfaces/ui/inav-link.interface';
import { AuthService } from '../../../shared/services/client/auth.service';
import * as classNames from "../../../shared/constants/class-names.constants"
import { Subject, takeUntil } from 'rxjs';

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
    { label: "Landing", route: "/" },
  ]

  protected isOpen = false;
  protected isAuthenticated = false;

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
    private router: Router,
    private authService: AuthService
  ) {
    this.isAuthenticated = this.authService.isAuthenticated;
  }

  public ngOnInit(): void {
    this.createSubscriptions(); 
  }

  public ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  private createSubscriptions(): void {
    this.authService.getIsAuthenticated$().pipe(takeUntil(this.unsubscribe$)).subscribe(result => {
      this.isAuthenticated = result;
    });
  }

  //#region Button Methods
  protected logout(): void {
    this.authService.logout().then((result) => {
      if(result.isSuccess) {
        this.router.navigate(['/login']);
      } else {
        // TODO: Handle logout failure
      }
    });
  }
  //#endregion

  //#region UI Methods
  protected toggleMenu() {
    if (!this.isOpen) {
      this.scrollLockService.disableScroll(this.scrollLockConfig);
    } else {
      this.scrollLockService.enableScroll();
    }

    this.isOpen = !this.isOpen;
  }
  //#endregion
}

