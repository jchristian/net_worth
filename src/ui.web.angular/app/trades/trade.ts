export interface Trade {
    id: number;
    aquireDate: Date;
    closingDate: Date;
    positionId: number;
    closingTransactionId: number;
    quantity: number;
    sellPrice: number;
    profileAndLoss: number;
}
