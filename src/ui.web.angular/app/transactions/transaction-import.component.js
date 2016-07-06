System.register(['@angular/core', 'ng2-file-upload/ng2-file-upload'], function(exports_1, context_1) {
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
    var core_1, ng2_file_upload_1;
    var TransactionImportComponent;
    return {
        setters:[
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (ng2_file_upload_1_1) {
                ng2_file_upload_1 = ng2_file_upload_1_1;
            }],
        execute: function() {
            TransactionImportComponent = (function () {
                function TransactionImportComponent() {
                    this.importSucceeded = new core_1.EventEmitter();
                }
                TransactionImportComponent.prototype.ngOnInit = function () {
                    var _this = this;
                    this.uploader = new ng2_file_upload_1.FileUploader({
                        url: 'http://localhost:60239/api/transactions/import',
                        removeAfterUpload: true,
                        autoUpload: true
                    });
                    this.uploader.onBeforeUploadItem = function (item) { return _this.onUpload(item); };
                };
                TransactionImportComponent.prototype.onUpload = function (item) {
                    var _this = this;
                    item.withCredentials = false;
                    item.onSuccess = function () {
                        _this.fileInput.nativeElement.value = null;
                        _this.uploadStatus = 'Import succeeded!';
                        _this.importSucceeded.emit({});
                    };
                    item.onError = function (response, status, headers) {
                        _this.fileInput.nativeElement.value = null;
                        _this.uploadStatus = 'Could not import transactions';
                    };
                    this.uploadStatus = 'Importing transactions';
                };
                __decorate([
                    core_1.ViewChild('fileInput'), 
                    __metadata('design:type', core_1.ElementRef)
                ], TransactionImportComponent.prototype, "fileInput", void 0);
                __decorate([
                    core_1.Output(), 
                    __metadata('design:type', core_1.EventEmitter)
                ], TransactionImportComponent.prototype, "importSucceeded", void 0);
                TransactionImportComponent = __decorate([
                    core_1.Component({
                        selector: 'transaction-import',
                        templateUrl: 'app/transactions/transaction-import.component.html',
                        directives: [ng2_file_upload_1.FILE_UPLOAD_DIRECTIVES]
                    }), 
                    __metadata('design:paramtypes', [])
                ], TransactionImportComponent);
                return TransactionImportComponent;
            }());
            exports_1("TransactionImportComponent", TransactionImportComponent);
        }
    }
});
//# sourceMappingURL=transaction-import.component.js.map