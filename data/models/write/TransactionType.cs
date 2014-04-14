namespace data.models.write
{
    public enum TransactionType
    {
        Missing = 0,
        Buy = 1,
        Sell = 2,
        Distribution_Dividend = 3,
        Distribution_LongTermCapGain = 4,
        Distribution_ShortTermCapGain = 5,
        Exchange = 6,
        Conversion = 7
    }
}