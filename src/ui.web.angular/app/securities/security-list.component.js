System.register(['@angular/core', '@angular/router', './security.service'], function(exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var __metadata = (this && this.__metadata) || function (k, v) {
        if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
    };
    var core_1, router_1, security_service_1;
    var SecurityListComponent;
    return {
        setters:[
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (router_1_1) {
                router_1 = router_1_1;
            },
            function (security_service_1_1) {
                security_service_1 = security_service_1_1;
            }],
        execute: function() {
            SecurityListComponent = (function () {
                function SecurityListComponent(securityService) {
                    this.securityService = securityService;
                    this.securities = [];
                }
                SecurityListComponent.prototype.ngOnInit = function () {
                    var _this = this;
                    this.securityService.getSecurities()
                        .subscribe(function (securities) { return _this.securities = securities; }, function (error) { return _this.errorMessage = error; });
                };
                SecurityListComponent = __decorate([
                    core_1.Component({
                        templateUrl: 'app/securities/security-list.component.html',
                        directives: [router_1.ROUTER_DIRECTIVES]
                    }), 
                    __metadata('design:paramtypes', [security_service_1.SecurityService])
                ], SecurityListComponent);
                return SecurityListComponent;
            }());
            exports_1("SecurityListComponent", SecurityListComponent);
        }
    }
});
//# sourceMappingURL=security-list.component.js.map