import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AuthUser } from 'src/app/_models/registerUser';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {
  registerForm: FormGroup;

  constructor(private accountService: AccountService, private formBuilder: FormBuilder, private toastr: ToastrService) {

  }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.registerForm = this.formBuilder.group({
      email: ['', Validators.email],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required]
    })
  }

  register() {
    let registerUser: AuthUser = this.registerForm.value as AuthUser;
    this.accountService.register(registerUser).subscribe(() => {
      this.toastr.success('Account has been created successfully.')
    }, error => {
      this.toastr.error(error.error.errors[0]);
    })
  }

}
