import { Component } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserService } from '../services/UserService';
import { UserModel } from '../models/UserModel';
import { Router } from '@angular/router';
import { usernameValidator } from '../misc/UserNameValidator';
import { passwordMatchValidator } from '../misc/PasswordMatchValidator';
// import { passwordMatchValidator } from '../misc/PasswordMatchValidator';

@Component({
  selector: 'app-add-user',
  imports: [FormsModule, ReactiveFormsModule],
  templateUrl: './add-user.html',
  styleUrl: './add-user.css'
})
export class AddUser {
  userForm: FormGroup;
  previewImage: string | null = null;

  constructor(private userService: UserService, private route: Router) {

    this.userForm = new FormGroup({
      firstName: new FormControl(null, Validators.required), 
      lastName: new FormControl(null, Validators.required),
      email:new FormControl(null, [Validators.required, Validators.pattern("^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$")]),
      username : new FormControl(null, [Validators.required, usernameValidator()]),
      password: new FormControl(null, Validators.required),
      confirmPassword: new FormControl(null, Validators.required),
      age: new FormControl(null, Validators.required),
      gender : new FormControl('male', Validators.required),
      role: new FormControl('user', Validators.required),
    },
    {
      validators: passwordMatchValidator
    });
  }

  public get firstName(): any {
    return this.userForm.get("firstName")
  }

  public get lastName(): any {
    return this.userForm.get("lastName")
  }
  public get email(): any {
    return this.userForm.get("email")
  }
  public get username(): any{
    return this.userForm.get("username");
  }
  public get password():any{
    return this.userForm.get("password");
  }
  public get confirmPassword():any{
    return this.userForm.get("confirmPassword");
  }

  // public get lname(): any {
  //   return this.userForm.get("lname")
  // }
  // public get age(): any {
  //   return this.userForm.get("age")
  // }
  // public get image(): any {
  //   return this.userForm.get("image")
  // }

  onSubmit() {
    if (this.userForm.valid) {
      const formValue = this.userForm.value;

      const newUser: UserModel = {
        id: 0,
        username: formValue.username,
        email: formValue.email,
        age: formValue.age,
        firstName: formValue.firstName,
        lastName: formValue.lastName,
        gender: formValue.gender,
        image: formValue.image,
        role: formValue.role,
      };

      this.userService.addUser(newUser);
      this.userForm.reset();
      this.route.navigateByUrl("/users");
    }
  }


  onImageSelected(event: any): void {
    const file: File = event.target.files[0];
    if (!file) return;

    const reader = new FileReader();
    reader.onload = () => {
      const base64 = reader.result as string;
      this.userForm.patchValue({ image: base64 });
      this.previewImage = base64;
    };
    reader.readAsDataURL(file);
  }
}
