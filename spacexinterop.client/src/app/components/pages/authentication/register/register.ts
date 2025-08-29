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
  ],
  templateUrl: './register.html',
  styleUrl: '../authentication.scss'
})
export class Register {
  protected invalidRegisterMessages: string[] | null = null;

  private unsubscribe$: Subject<void> = new Subject<void>();
  
  protected readonly registerForm: FormGroup = new FormGroup({
    username: new FormControl<string>(commonConst.EMPTY_STRING, [Validators.required, Validators.minLength(commonConst.MIN_USERNAME_LENGTH), Validators.maxLength(commonConst.MAX_USERNAME_LENGTH)]),
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
      username: this.registerForm.get('username')?.value,
      email: this.registerForm.get('email')?.value,
      password: this.registerForm.get('password')?.value,
      confirmPassword: this.registerForm.get('confirmPassword')?.value,
    };

    await this.authService.register(request).then((result) => {
      if(result.isSuccess) {
        this.invalidRegisterMessages = null;
        this.router.navigate(['/']);
      } else {
        this.invalidRegisterMessages = result.error.message.split(/\r?\n/).filter(line => line.trim() !== commonConst.EMPTY_STRING) ?? [];
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
