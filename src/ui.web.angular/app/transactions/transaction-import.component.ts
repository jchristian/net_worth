import { Component, OnInit, Output, EventEmitter, ElementRef, ViewChild } from '@angular/core';

import { FileUploader, FILE_UPLOAD_DIRECTIVES, FileItem } from 'ng2-file-upload/ng2-file-upload';

@Component({
    selector: 'transaction-import',
    templateUrl: 'app/transactions/transaction-import.component.html',
    directives: [FILE_UPLOAD_DIRECTIVES]
})
export class TransactionImportComponent implements OnInit {
    uploader: FileUploader;
    uploadStatus: string;
    @ViewChild('fileInput') fileInput: ElementRef;
    @Output() importSucceeded: EventEmitter<any> = new EventEmitter<any>();

    ngOnInit() {
        this.uploader = new FileUploader({
            url: 'http://localhost:60239/api/transactions/import',
            removeAfterUpload: true,
            autoUpload: true
        });
        this.uploader.onBeforeUploadItem = (item) => this.onUpload(item);
    }

    onUpload(item: FileItem) {
        item.withCredentials = false;

        item.onSuccess = () => {
            this.fileInput.nativeElement.value = null;
            this.uploadStatus = 'Import succeeded!';
            this.importSucceeded.emit({});
        };

        item.onError = (response, status, headers) => {
            this.fileInput.nativeElement.value = null;
            this.uploadStatus = 'Could not import transactions';
        };

        this.uploadStatus = 'Importing transactions';
    }
}
