import { Component } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserService } from '../services/UserService';
import { UserModel } from '../models/UserModel';
import { Router } from '@angular/router';

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
      lname: new FormControl(null, Validators.required),
      age: new FormControl(null, Validators.required),
      image: new FormControl(null, Validators.required),
      role: new FormControl('user', Validators.required),
      address: new FormControl(null, Validators.required),
      city: new FormControl(null, Validators.required),
      state: new FormControl(null, Validators.required),
      stateCode: new FormControl(null, Validators.required),
      postalCode: new FormControl(null, Validators.required),
      country: new FormControl(null, Validators.required)
    });


  }

  // public get firstName(): any {
  //   return this.userForm.get("firstName")
  // }

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
        username: formValue.firstName.toLowerCase() + formValue.lname.toLowerCase(),
        email: formValue.firstName.toLowerCase() + '@example.com',
        age: formValue.age,
        firstName: formValue.firstName,
        lastName: formValue.lname,
        gender: '', 
        image: formValue.image,
        role: '', 
        address: {
          address: formValue.address,
          city: formValue.city,
          state: formValue.state,
          stateCode: formValue.stateCode,
          postalCode: formValue.postalCode,
          country: formValue.country
        }
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
