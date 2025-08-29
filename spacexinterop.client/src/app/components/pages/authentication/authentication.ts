import { Component } from '@angular/core';
import { FormGroup, Validators, ReactiveFormsModule, FormControl } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { Router } from '@angular/router';
import { AuthService } from '../../../shared/services/client/auth.service';
// import { SignupRequest } from '../../../shared/classes/models/requests/SignupRequest.model';
import { GenericButton } from '../../core/generic-button/generic-button';
import { PASSWORD_REGEX } from '../../../shared/constants/regex.constants';
import * as commonConst from '../../../shared/constants/common.constants';
import { LoginRequest } from '../../../shared/classes/models/requests/LoginRequest.model';

@Component({
  selector: 'app-authentication',
  imports: [
    MatCardModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatInputModule,
    GenericButton,
  ],
  templateUrl: './authentication.html',
  styleUrl: './authentication.scss'
})
export class Authentication {
  protected invalidLogin: boolean = false;

  protected readonly loginForm: FormGroup = new FormGroup({
    email: new FormControl<string>(commonConst.EMPTY_STRING, [Validators.required, Validators.email]),
    password: new FormControl<string>(commonConst.EMPTY_STRING, [Validators.required, Validators.minLength(commonConst.MIN_PASSWORD_LENGTH), Validators.maxLength(commonConst.MAX_PASSWORD_LENGTH), Validators.pattern(PASSWORD_REGEX)]),
  });

  // protected readonly signupForm: FormGroup = new FormGroup({
  //   username: new FormControl<string>(commonConst.EMPTY_STRING, [Validators.required, Validators.minLength(commonConst.MIN_USERNAME_LENGTH), Validators.maxLength(commonConst.MAX_USERNAME_LENGTH)]),
  //   email: new FormControl<string>(commonConst.EMPTY_STRING, [Validators.required, Validators.email]),
  //   password: new FormControl<string>(commonConst.EMPTY_STRING, [Validators.required, Validators.minLength(commonConst.MIN_PASSWORD_LENGTH), Validators.maxLength(commonConst.MAX_PASSWORD_LENGTH), Validators.pattern(PASSWORD_REGEX)]),
  // });

  constructor(
    private authService: AuthService,
    private router: Router
  ) {

  }

  async onSubmit(): Promise<void> {
    if (this.loginForm?.valid) {
      const request: LoginRequest = new LoginRequest(
        this.loginForm.get('email')?.value,
        this.loginForm.get('password')?.value,
        true // TODO: Implement remember me functionality
      );

      await this.authService.login(request).then(() => {
        this.router.navigate(['/']);
      });
    }
  }
}
