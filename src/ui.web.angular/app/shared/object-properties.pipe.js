System.register(['@angular/core'], function(exports_1, context_1) {
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
    var core_1;
    var PropertyNamesPipe, PropertyValuesPipe, CamelToTitlePipe;
    return {
        setters:[
            function (core_1_1) {
                core_1 = core_1_1;
            }],
        execute: function() {
            PropertyNamesPipe = (function () {
                function PropertyNamesPipe() {
                }
                PropertyNamesPipe.prototype.transform = function (value, args) {
                    return Object.keys(value);
                };
                PropertyNamesPipe = __decorate([
                    core_1.Pipe({ name: 'propertyNames' }), 
                    __metadata('design:paramtypes', [])
                ], PropertyNamesPipe);
                return PropertyNamesPipe;
            }());
            exports_1("PropertyNamesPipe", PropertyNamesPipe);
            PropertyValuesPipe = (function () {
                function PropertyValuesPipe() {
                }
                PropertyValuesPipe.prototype.transform = function (value, args) {
                    return Object.keys(value).map(function (key) { return value[key]; });
                };
                PropertyValuesPipe = __decorate([
                    core_1.Pipe({ name: 'propertyValues' }), 
                    __metadata('design:paramtypes', [])
                ], PropertyValuesPipe);
                return PropertyValuesPipe;
            }());
            exports_1("PropertyValuesPipe", PropertyValuesPipe);
            CamelToTitlePipe = (function () {
                function CamelToTitlePipe() {
                }
                CamelToTitlePipe.prototype.transform = function (value, args) {
                    return value.map(function (title) {
                        var result = title.replace(/([A-Z])/g, ' $1');
                        return result.charAt(0).toUpperCase() + result.slice(1);
                    });
                };
                CamelToTitlePipe = __decorate([
                    core_1.Pipe({ name: 'camelToTitle' }), 
                    __metadata('design:paramtypes', [])
                ], CamelToTitlePipe);
                return CamelToTitlePipe;
            }());
            exports_1("CamelToTitlePipe", CamelToTitlePipe);
        }
    }
});
//# sourceMappingURL=object-properties.pipe.js.map