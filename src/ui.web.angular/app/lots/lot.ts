export interface Lot {
        id: number;
        brokerageTransactionId: number;
        isOpen: boolean;
        remainingShares: number;
        remainingAmount: number;
}
