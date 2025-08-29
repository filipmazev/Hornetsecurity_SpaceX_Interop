import { EMPTY_STRING } from "../../constants/common.constants";
import { IAccessibilitySettingItem } from "../../interfaces/settings/accessibility-setting-item.interface";
import { MatSelectType } from "../../types/ui.types";
import { SelectionChange } from "../../types/ui.types";

export class AccessibilitySettingItem implements IAccessibilitySettingItem {
    selfIdentifier: string; 
    state: boolean;
    
    title?: string;
    matIcons?: string;
    
    ariaLabel: string;

    type: MatSelectType;

    changes?: (event: SelectionChange, value: any) => any;

    filterStyle?: string;

    constructor(value?: IAccessibilitySettingItem) {
        this.selfIdentifier = value?.selfIdentifier ?? EMPTY_STRING;
        this.state = value?.state ?? false;

        this.title = value?.title;
        this.matIcons = value?.matIcons;

        this.ariaLabel = value?.ariaLabel ?? EMPTY_STRING;

        this.type = value?.type ?? 'toggle';

        this.changes = value?.changes;

        this.filterStyle = value?.filterStyle;
    }
}