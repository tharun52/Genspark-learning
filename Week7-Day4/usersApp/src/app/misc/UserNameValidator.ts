import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export function usernameValidator():ValidatorFn{
    const bannedWords: string[] = ["admin", "root", "user", "moderator"];
    return(control:AbstractControl):ValidationErrors|null => {
        const value = control.value;
        if(bannedWords.includes(value))
            return{bannedWordsError:"The username cannot contain words like admin, root, user, moderator"}
        return null;
    }
}