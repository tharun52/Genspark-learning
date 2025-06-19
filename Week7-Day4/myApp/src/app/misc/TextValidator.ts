import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export function TextValidator():ValidatorFn
{
    return (control:AbstractControl):ValidationErrors | null => {
        const value = control.value as string;
        if(value?.length<6)
            return {lenError:'Password is less than 6'}
        return null;
    };
}