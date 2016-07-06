import { Component, OnInit } from '@angular/core';
import { ROUTER_DIRECTIVES } from '@angular/router';

import { SecurityService } from './security.service';
import { Security } from './security';

@Component({
    templateUrl: 'app/securities/security-list.component.html',
    directives: [ROUTER_DIRECTIVES]
})
export class SecurityListComponent implements OnInit {
    securities: Security[] = [];
    errorMessage: string;

    constructor(private securityService: SecurityService) { }

    ngOnInit() {
        this.securityService.getSecurities()
                            .subscribe((securities: Security[]) => this.securities = securities,
                                        error => this.errorMessage = <any>error);
    }
}
