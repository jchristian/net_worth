import { Transaction } from './transaction';

export interface OpenTransaction {
    transaction: Transaction;
    tradedShares: number;
    remainingShares: number;
}
