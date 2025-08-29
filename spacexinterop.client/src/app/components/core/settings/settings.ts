import { NgClass } from '@angular/common';
import { AfterViewInit, ChangeDetectorRef, Component, OnDestroy } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatCheckbox } from '@angular/material/checkbox';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatRadioModule } from '@angular/material/radio';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatTooltipModule } from '@angular/material/tooltip';
import { Subject, takeUntil } from 'rxjs';
import { SiteSettings } from '../../../shared/classes/domain/site-settings';
import { DbKeys } from '../../../shared/enums/db/db-keys.enum';
import { DbStores } from '../../../shared/enums/db/db-stores.enum';
import { MatIcons } from '../../../shared/enums/ui/mat-icons.enum';
import { IDarkModeThemeSettings } from '../../../shared/interfaces/settings/dark-mode-theme-settings.interface';
import { ISiteSettings } from '../../../shared/interfaces/settings/site-settings.interface';
import { IThemeSettings } from '../../../shared/interfaces/settings/theme-settings.interface';
import { DbService } from '../../../shared/services/core/db.service';
import { AccessabilitySettingsBuilderService } from '../../../shared/services/core/ui/accessability-settings-builder.service';
import { DeviceThemeService } from '../../../shared/services/core/ui/device-theme.service';
import { DeviceTheme } from '../../../shared/types/device.types';

@Component({
  selector: 'settings',
  standalone: true,
  imports: [
    MatMenuModule,
    MatIconModule,
    MatSlideToggleModule,
    MatRadioModule,
    MatCheckbox,
    MatTooltipModule,
    FormsModule,
    NgClass,
  ],
  templateUrl: './settings.html',
  styleUrl: './settings.scss'
})
export class Settings implements OnDestroy, AfterViewInit {
  protected settings: ISiteSettings = new SiteSettings();
  protected MatIcons = MatIcons;

  protected initialized: boolean = false;

  private autoDarkThemeSubscribe$ = new Subject<void>();

  private unsubscribe$ = new Subject<void>();
  public selectedThemeIndex: number = -1;

  constructor(
    private cdr: ChangeDetectorRef,
    private accessabilitySettings: AccessabilitySettingsBuilderService,
    private deviceThemeService: DeviceThemeService,
    private dbService: DbService
  ) {
  }

  ngAfterViewInit(): void {
    this.InitializeSettings();
  }

  ngOnDestroy() {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();

    this.autoDarkThemeSubscribe$?.next();
    this.autoDarkThemeSubscribe$?.complete();
  }

  private async InitializeSettings(): Promise<void> {
    this.settings.accessibility.items = await this.accessabilitySettings.build();
    await this.dbService.fetchData(DbStores.THEME_SETTINGS, DbKeys.theme).then(async (theme: string) => {
      if (theme) {
        this.settings.theme = JSON.parse(theme) as IThemeSettings;
        this.settings.theme.state = false;

        for(let i = 0; i < this.settings.theme.darkMode.theming.length; i++) {
          const item = this.settings.theme.darkMode.theming[i];
          if (item.selected) {
            this.selectedThemeIndex = i;
          }
        }

        if(this.settings.theme.autoDarkMode) {
          this.startAutoDarkThemeSubscription();
        }

        this.toggleDarkMode();

        this.cdr.detectChanges();
      } else {
        this.selectedThemeIndex = 0;
        
        await this.saveSetting(DbKeys.theme, this.settings.theme);
      }
      this.initialized = true;
    });

    this.settings.accessibility.state = false;
  }

  private async saveSetting(key: DbKeys, value: any): Promise<void> {
    return new Promise(async (resolve, reject) => {
      try {
        await this.dbService.saveData(DbStores.THEME_SETTINGS, key, JSON.stringify(value));
        resolve();
      } catch (error) {
        reject(error);
      }
    });
  }

  private handleDeviceThemeChange(theme: DeviceTheme = this.settings.theme.deviceTheme): void {
    this.settings.theme.darkMode.state = this.settings.theme.autoDarkMode && theme === 'dark';
    if (this.initialized) {
      this.saveThemeSetting();
    }
    this.toggleDarkMode();
  }

  protected async toggledAutoDarkTheme(): Promise<void> {
    if (this.settings.theme.autoDarkMode) {
      this.startAutoDarkThemeSubscription();
      this.handleDeviceThemeChange(this.deviceThemeService.getDeviceTheme());
    } else {
      this.autoDarkThemeSubscribe$?.next();
      this.autoDarkThemeSubscribe$?.complete();
    }
    return await this.saveThemeSetting();
  }

  private startAutoDarkThemeSubscription(): void {
    this.autoDarkThemeSubscribe$ = new Subject<void>();

    this.deviceThemeService.getDeviceTheme$().pipe(takeUntil(this.autoDarkThemeSubscribe$), takeUntil(this.unsubscribe$)).subscribe(deviceTheme => {
      if(this.settings.theme.autoDarkMode) {
        this.settings.theme.deviceTheme = deviceTheme;
        this.handleDeviceThemeChange();
      }
    });
  }

  protected async saveThemeSetting(): Promise<void> {
    return await this.saveSetting(DbKeys.theme, this.settings.theme);
  }

  protected async onThemeChange(index: number): Promise<void> {
    this.settings.theme.darkMode.theming.forEach((item, i) => {
      item.selected = i === index;
    });
    await this.saveThemeSetting();
  }

  protected toggleDarkMode(setting?: IDarkModeThemeSettings, state?: boolean): void {
    const selectedTheme: string | undefined = setting?.className
      ? setting.className
      : this.settings.theme.darkMode.theming.find(theme => theme.selected)?.className;

    if (selectedTheme) {
      document.body.classList.toggle(selectedTheme, state ?? this.settings.theme.darkMode.state);
      this.settings.theme.darkMode.theming.filter(theme => theme.className !== selectedTheme)?.forEach(theme => {
        document.body.classList.toggle(theme.className, false);
      });
    }
  }

  protected toggleLanguageSettingState(): void {
    this.settings.accessibility.state = false;
    this.settings.theme.state = false;
  }

  protected toggleAccessibilitySettingState(): void {
    this.settings.accessibility.state = !this.settings.accessibility.state;
    this.settings.theme.state = false;
  }

  protected toggleThemeSettingState(): void {
    this.settings.theme.state = !this.settings.theme.state;
    this.settings.accessibility.state = false;
  }
}