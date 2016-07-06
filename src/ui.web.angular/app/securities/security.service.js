System.register(['@angular/core', '../services/basic-http-service'], function(exports_1, context_1) {
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
    var core_1, basic_http_service_1;
    var SecurityService;
    return {
        setters:[
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (basic_http_service_1_1) {
                basic_http_service_1 = basic_http_service_1_1;
            }],
        execute: function() {
            SecurityService = (function () {
                function SecurityService(httpService) {
                    this.httpService = httpService;
                    this.securities_url = 'http://localhost:60239/api/securities';
                }
                SecurityService.prototype.getSecurities = function () {
                    return this.httpService.get(this.securities_url);
                };
                SecurityService.prototype.getSecurityDescriptions = function (securityId) {
                    var url = 'http://localhost:60239/api/security/' + securityId + '/descriptions';
                    return this.httpService.get(url);
                };
                SecurityService = __decorate([
                    core_1.Injectable(), 
                    __metadata('design:paramtypes', [basic_http_service_1.BasicHttpService])
                ], SecurityService);
                return SecurityService;
            }());
            exports_1("SecurityService", SecurityService);
        }
    }
});
//# sourceMappingURL=security.service.js.map