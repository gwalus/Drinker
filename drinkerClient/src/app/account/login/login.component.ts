import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { AuthUser } from 'src/app/_models/registerUser';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css', '../style.css']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;

  constructor(private modalService: NgbModal, private accountService: AccountService, private formBuilder: FormBuilder, private toastr: ToastrService,
    private router: Router) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.loginForm = this.formBuilder.group({
      email: ['', Validators.email],
      password: ['', Validators.required]
    })
  }

  // convenience getter for easy access to form fields
  get getFormControl() { return this.loginForm.controls; }

  login() {
    let loginUser = this.loginForm.value as AuthUser;
    this.accountService.login(loginUser).subscribe(() => {
      this.toastr.success('Login successfully');
      this.modalService.dismissAll();
      this.router.navigateByUrl('');
    }, error => {
      this.toastr.error(error.error.errors[0]);
    })
  }

  openRegistraionContent(registrationContent: any) {
    this.modalService.open(registrationContent, { scrollable: true });
  }
}
