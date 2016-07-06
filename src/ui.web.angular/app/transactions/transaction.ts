import { Trade } from '../trades/trade.ts';

export interface Transaction {
    id: number;
    accountId: number;
    tradeDate: Date;
    processDate: Date;
    transactionType: number;
    transactionDescription: string;
    trades: Trade[];
    securityId: number;
    securityDescription: string;
    sharePrice: number;
    shares: number;
    grossAmount: number;
    netAmount: number;
}
