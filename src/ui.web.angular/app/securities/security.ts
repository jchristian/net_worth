import { SecurityDescription } from './descriptions/security-description';

export interface Security {
    id: number;
    ticker: string;
    name: string;
    securityDescriptions: SecurityDescription[];
}
