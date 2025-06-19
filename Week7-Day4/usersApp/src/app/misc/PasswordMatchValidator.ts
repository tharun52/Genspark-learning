// import { AbstractControl, FormGroup, ValidationErrors, ValidatorFn } from "@angular/forms";

import { AbstractControl, FormGroup, ValidationErrors, ValidatorFn } from "@angular/forms";

export const passwordMatchValidator: ValidatorFn = (control: AbstractControl): ValidationErrors | null => {
    const form = control as FormGroup;
    const password = form.get('password')?.value;
    const confirmPassword = form.get('confirmPassword')?.value;

    if (password && confirmPassword && password !== confirmPassword) {
        return { passwordMismatch: true };
    }
    return null;
};
// export function passwordMatchValidator(passwordString:string, confirmPasswordString:string) : ValidatorFn
// {
//     return(control:AbstractControl):{ [key: string]: any } | null => {
//         const password = control.get(passwordString)?.value;
//         const confirmPassword = control.get(confirmPasswordString)?.value;
//         if(password!=confirmPassword)
//             return {passwordMistach:"Password and Confirm Password should be the same"};
//         return null
//     }
// }
