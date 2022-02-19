import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { Observer } from "rxjs/internal/types";
import { Store } from "../services/store.service";
import { LoginRequest } from "../shared/LoginResults";

@Component({
    selector: "login-page",
    templateUrl: "loginPage.component.html"
})
export class LoginPage {
    constructor(public store: Store, private router: Router) { }

    public creds: LoginRequest = {
        username: "",
        password: ""
    }

    public errorMessage = "";

    onLogin() {
        this.store.login(this.creds)
            .subscribe({ // the deprecated descriptions are shown by IntelliSense in all cases, see: https://github.com/ReactiveX/rxjs/issues/4159#issuecomment-466630791
                next: this.handleLoginSuccess.bind(this),
                error: this.handleLoginError.bind(this)
            });
    }

    private handleLoginSuccess() {
        if (this.store.order.items.length > 0) {
            this.router.navigate(["checkout"]);
            return;
        }

        this.router.navigate([""]);
    }

    private handleLoginError(error: any) {
        console.log(error);
        this.errorMessage = "Failed to login";
    }
}