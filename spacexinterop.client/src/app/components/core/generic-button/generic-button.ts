import { NgClass } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { EMPTY_STRING } from '../../../shared/constants/common.constants';
import { ButtonType } from '../../../shared/types/ui.types';
import { MatIcon } from '@angular/material/icon';

@Component({
  selector: 'generic-button',
  imports: [
    NgClass,
    MatIcon,
  ],
  templateUrl: './generic-button.html',
  styleUrl: './generic-button.scss'
})
export class GenericButton {
  @Input() text?: string;
  @Input() matIcon?: string;
  @Input() classes: string = EMPTY_STRING;
  @Input() styles: string = EMPTY_STRING;
  @Input() style: ButtonType = 'default';
  @Input() id: string = EMPTY_STRING;
  @Input() disabled: boolean = false;

  @Output() onClick: EventEmitter<void> = new EventEmitter<void>();

  constructor() {
  }

  protected onClickEvent() {
    if (!this.disabled) {
      this.onClick.emit();
    }
  }
}
