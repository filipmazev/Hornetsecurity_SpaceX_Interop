import { DarkModeTheme } from "../../enums/settings/dark-mode-theme.enum";

export interface IDarkModeThemeSettings {
    className: string;
    theme: DarkModeTheme;
    selected: boolean;

    title?: string;
    description?: string;
}