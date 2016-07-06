import { Component } from '@angular/core';
import { OnActivate, RouteSegment } from '@angular/router';

import { SecurityService } from '../security.service';
import { SecurityDescription } from './security-description';

@Component({
    templateUrl: 'app/securities/descriptions/security-description.component.html',
})
export class SecurityDescriptionComponent implements OnActivate {
    descriptions: SecurityDescription[] = [];
    securityId: number;
    errorMessage: string;

    constructor(private securityService: SecurityService) { }

    routerOnActivate(segment: RouteSegment) {
        this.securityId = +segment.getParam('securityId');
        this.getSecurityDescriptions(this.securityId);
    }

    getSecurityDescriptions(securityId: number) {
        this.securityService.getSecurityDescriptions(securityId)
                            .subscribe((descriptions: SecurityDescription[]) => this.descriptions = descriptions,
                                       error => this.errorMessage = <any>error);
    }
}
