import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { GenericButton } from '../../../core/generic-button/generic-button';
import { PASSWORD_REGEX } from '../../../../shared/constants/regex.constants';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../../shared/services/client/auth.service';
import { LoginRequest } from '../../../../shared/classes/models/requests/LoginRequest.model';
import { MatCheckbox } from '@angular/material/checkbox';
import * as commonConst from '../../../../shared/constants/common.constants';

@Component({
  selector: 'app-login',
  imports: [
    MatCardModule,
    MatCheckbox,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatInputModule,
    RouterLink,
    GenericButton,
  ],
  templateUrl: './login.html',
  styleUrl: '../authentication.scss'
})
export class Login {
  protected invalidLogin: boolean = false;

  protected readonly loginForm: FormGroup = new FormGroup({
    email: new FormControl<string>(commonConst.EMPTY_STRING, [Validators.required, Validators.email]),
    password: new FormControl<string>(commonConst.EMPTY_STRING, [Validators.required, Validators.minLength(commonConst.MIN_PASSWORD_LENGTH), Validators.maxLength(commonConst.MAX_PASSWORD_LENGTH), Validators.pattern(PASSWORD_REGEX)]),
    rememberMe: new FormControl<boolean>(false)
  });

  constructor(
    private authService: AuthService,
    private router: Router
  ) {

  }

  async login(): Promise<void> {
    if (this.loginForm.invalid) {
      return;
    }

    const request: LoginRequest = new LoginRequest(
      this.loginForm.get('email')?.value,
      this.loginForm.get('password')?.value,
      this.loginForm.get('rememberMe')?.value
    );

    await this.authService.login(request).then((result) => {
      if (result.isSuccess) {
        this.invalidLogin = false;
        this.router.navigate(['/']);
      } else {
        this.invalidLogin = true;
        this.loginForm.reset();
      }
    });
  }
}
