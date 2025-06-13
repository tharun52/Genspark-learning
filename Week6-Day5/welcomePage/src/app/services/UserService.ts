import { BehaviorSubject, Observable } from "rxjs";
import { UserLoginModel } from "../models/UserLoginModel";

export class UserService {
    private dummyUser: UserLoginModel[] = [
        { username: "tharun@gmail.com", password: "tharun123" },
        { username: "user@gmail.com", password: "user123" }
    ];
    private usernameSubject = new BehaviorSubject<string | null>(null);
    username$: Observable<string | null> = this.usernameSubject.asObservable();

    validateUserLogin(user: UserLoginModel) {
        if (user.username.length < 3) {
            this.usernameSubject.error("Username too short");
            this.usernameSubject.next(null);
        }
        if (this.dummyUser.find(u => u.username === user.username && u.password == user.password)) {
            this.usernameSubject.next(user.username);
        }
        else {
            this.usernameSubject.error("Invalid Username password");
            this.usernameSubject.next(null);
        }
    }
}