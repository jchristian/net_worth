System.register(['@angular/core', '../security.service'], function(exports_1, context_1) {
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
    var core_1, security_service_1;
    var SecurityDescriptionComponent;
    return {
        setters:[
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (security_service_1_1) {
                security_service_1 = security_service_1_1;
            }],
        execute: function() {
            SecurityDescriptionComponent = (function () {
                function SecurityDescriptionComponent(securityService) {
                    this.securityService = securityService;
                    this.descriptions = [];
                }
                SecurityDescriptionComponent.prototype.routerOnActivate = function (segment) {
                    this.securityId = +segment.getParam('securityId');
                    this.getSecurityDescriptions(this.securityId);
                };
                SecurityDescriptionComponent.prototype.getSecurityDescriptions = function (securityId) {
                    var _this = this;
                    this.securityService.getSecurityDescriptions(securityId)
                        .subscribe(function (descriptions) { return _this.descriptions = descriptions; }, function (error) { return _this.errorMessage = error; });
                };
                SecurityDescriptionComponent = __decorate([
                    core_1.Component({
                        templateUrl: 'app/securities/descriptions/security-description.component.html',
                    }), 
                    __metadata('design:paramtypes', [security_service_1.SecurityService])
                ], SecurityDescriptionComponent);
                return SecurityDescriptionComponent;
            }());
            exports_1("SecurityDescriptionComponent", SecurityDescriptionComponent);
        }
    }
});
//# sourceMappingURL=security-description.component.js.map