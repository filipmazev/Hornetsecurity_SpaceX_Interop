import { Component } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';
import { AuthService } from '../../../../shared/services/client/auth.service';
import { FormGroup, FormControl, Validators, ReactiveFormsModule } from '@angular/forms';
import { PASSWORD_REGEX } from '../../../../shared/constants/regex.constants';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { GenericButton } from '../../../core/generic-button/generic-button';
import { MatCardModule } from '@angular/material/card';
import { RegisterRequest } from '../../../../shared/classes/models/requests/RegisterRequest';
import { ResultStatusEnum } from '../../../../shared/enums/api/ResultStatusEnum';
import { NgClass } from '@angular/common';
import * as commonConst from '../../../../shared/constants/common.constants';

@Component({
  selector: 'app-register',
  imports: [
    MatCardModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatInputModule,
    RouterLink,
    GenericButton,
    NgClass
  ],
  templateUrl: './register.html',
  styleUrl: '../authentication.scss'
})
export class Register {
  protected invalidRegisterMessages: string[] | null = null;
  protected offerLogInInstead: boolean = false;

  private unsubscribe$: Subject<void> = new Subject<void>();
  
  protected readonly registerForm: FormGroup = new FormGroup({
    firstName: new FormControl<string>(commonConst.EMPTY_STRING, [Validators.required, Validators.maxLength(commonConst.MAX_IDENTITY_STRING_LENGTH)]),
    lastName: new FormControl<string>(commonConst.EMPTY_STRING, [Validators.required, Validators.maxLength(commonConst.MAX_IDENTITY_STRING_LENGTH)]),
    email: new FormControl<string>(commonConst.EMPTY_STRING, [Validators.required, Validators.email]),
    password: new FormControl<string>(commonConst.EMPTY_STRING, [Validators.required, Validators.minLength(commonConst.MIN_PASSWORD_LENGTH), Validators.maxLength(commonConst.MAX_PASSWORD_LENGTH), Validators.pattern(PASSWORD_REGEX)]),
    confirmPassword: new FormControl<string | undefined>(undefined),
  });

  constructor(
    private authService: AuthService,
    private router: Router
  ) {

  }

  public ngOnInit(): void {
    this.createSubscriptions();
  }

  public ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  private createSubscriptions() {
    this.registerForm.get('confirmPassword')?.valueChanges.pipe(takeUntil(this.unsubscribe$)).subscribe(value => {
      if(value === undefined) return;
      this.validateConfirmPassword(value);
    });
  }

  protected async register(): Promise<void> {
    if (this.registerForm.invalid) {
      return;
    }

    const request: RegisterRequest = {
      firstName: this.registerForm.get('firstName')?.value,
      lastName: this.registerForm.get('lastName')?.value,
      email: this.registerForm.get('email')?.value,
      password: this.registerForm.get('password')?.value,
      confirmPassword: this.registerForm.get('confirmPassword')?.value,
    };

    await this.authService.register(request).then((result) => {
      if(result.isSuccess) {
        this.offerLogInInstead = false;
        this.invalidRegisterMessages = null;
        this.router.navigate(['/']);
      } else {
        this.invalidRegisterMessages = result.error?.messages ?? [];
        this.offerLogInInstead = result.status === ResultStatusEnum.EmailAlreadyExists;
      }
    });
  }

  //#region Form Validators

  private validateConfirmPassword(confirmPassword: string): void {
    const password = this.registerForm.get('password')?.value;
    if (password !== confirmPassword) {
      this.registerForm.get('confirmPassword')?.setErrors({ mismatch: true });
    }
  }

  //#endregion
}
