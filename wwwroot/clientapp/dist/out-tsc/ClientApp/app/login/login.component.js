import { __decorate } from "tslib";
import { Component } from "@angular/core";
var Login = /** @class */ (function () {
    function Login(data, router) {
        this.data = data;
        this.router = router;
        this.errorMessage = "";
        this.creds = {
            username: "",
            password: ""
        };
    }
    Login.prototype.onLogin = function () {
        var _this = this;
        this.data.login(this.creds)
            .subscribe(function (success) {
            if (success) {
                if (_this.data.order.items.length == 0) {
                    _this.router.navigate([""]);
                }
                else {
                    _this.router.navigate(["checkout"]);
                }
            }
        }, function (err) { return _this.errorMessage = "Failed to login"; });
    };
    Login = __decorate([
        Component({
            selector: "Login",
            templateUrl: "login.component.html"
        })
    ], Login);
    return Login;
}());
export { Login };
//# sourceMappingURL=login.component.js.map