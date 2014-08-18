using data.models.contexts;
using data.models.write;

namespace data.Migrations
{
    public class VanguardSeedData
    {
        public void Seed(DataContext context)
        {
            var money_market = new Security { Name = "Vanguard Prime Money Market Fund", Ticker = "VMMXX" };
            var total_stock_market_admiral = new Security { Name = "Vanguard Total Stock Market Index Fund Admiral Shares", Ticker = "VTSAX" };
            var total_stock_market_investor = new Security { Name = "Vanguard Total Stock Market Index Fund Investor Shares", Ticker = "VTSMX" };
            var sp5_admiral = new Security { Name = "Vanguard 500 Index Fund Admiral Shares", Ticker = "VFIAX" };
            var international_admiral = new Security { Name = "Vanguard Total International Stock Index Fund Admiral Shares", Ticker = "VTIAX" };
            var short_term_bond = new Security { Name = "Vanguard Short-Term Investment-Grade Fund Investor Shares", Ticker = "VFSTX" };
            var reit = new Security { Name = "Vanguard REIT Index Fund Admiral Shares", Ticker = "VGSLX" };

            context.Securities.AddRange(new[]
                                        {
                                            money_market, total_stock_market_admiral, total_stock_market_investor,
                                            sp5_admiral, international_admiral, short_term_bond, reit
                                        });
            context.SaveChanges();

            context.SecurityDescriptions.AddRange(new[]
                                                  {
                                                      new SecurityDescription { SecurityId = money_market.Id, Description = "Prime Money Mkt Fund" },
                                                      new SecurityDescription { SecurityId = sp5_admiral.Id, Description = "500 Index Fund Adm" },
                                                      new SecurityDescription { SecurityId = short_term_bond.Id, Description = "Short-Term Bond Index Adm" },
                                                      new SecurityDescription { SecurityId = reit.Id, Description = "REIT Index Fund Adm" },
                                                      new SecurityDescription { SecurityId = total_stock_market_admiral.Id, Description = "Total Stock Mkt Idx Adm" },
                                                      new SecurityDescription { SecurityId = total_stock_market_investor.Id, Description = "Total Intl Stock Ix Inv" },
                                                      new SecurityDescription { SecurityId = international_admiral.Id, Description = "Tot Intl Stock Ix Admiral" }
                                                  });
            context.SaveChanges();

            context.TransactionMatches.AddRange(new[]
                                                {
                                                    new TransactionMatch { TransactionType = TransactionType.Distribution_Dividend, ContainsMatchString = "dividend", TransactionMatchType = TransactionMatchType.ContainsMatch },
                                                    new TransactionMatch { TransactionType = TransactionType.Distribution_ShortTermCapGain, ContainsMatchString = "st cap gain", TransactionMatchType = TransactionMatchType.ContainsMatch },
                                                    new TransactionMatch { TransactionType = TransactionType.Distribution_LongTermCapGain, ContainsMatchString = "lt cap gain", TransactionMatchType = TransactionMatchType.ContainsMatch }
                                                });
            context.SaveChanges();
        }
    }
}