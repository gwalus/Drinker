import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AuthUser } from 'src/app/_models/registerUser';
import { AccountService } from 'src/app/_services/account.service';
import { MustMatch } from './_helpers/must-match.validator';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css', '../style.css']
})
export class RegistrationComponent implements OnInit {
  registerForm: FormGroup;

  constructor(private router: Router, private modalService: NgbModal, private accountService: AccountService, private formBuilder: FormBuilder, private toastr: ToastrService) {}

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.registerForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', Validators.required]
    }, {
        validator: MustMatch('password', 'confirmPassword')
    });
  }

  // convenience getter for easy access to form fields
  get getFormControl() { return this.registerForm.controls; }

  register() {
    let registerUser: AuthUser = this.registerForm.value as AuthUser;
    this.accountService.register(registerUser).subscribe(() => {
      this.toastr.success('Account has been created successfully.');
      this.modalService.dismissAll();
      this.router.navigateByUrl('');
    }, error => {
      this.toastr.error(error.error.errors[0]);
    })
  }

}
