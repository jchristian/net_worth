using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using data.models.contexts;
using data.models.read;
using data.models.write;

namespace ui.web.Controllers
{
    public class SecuritiesController : Controller
    {
        DataContext context;

        public SecuritiesController(DataContext context)
        {
            this.context = context;
        }

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult List()
        {
            return View(context.Securities.ToList());
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "Ticker, Name")]Security security)
        {
            if (!context.Securities.Any(x => x.Ticker == security.Ticker))
            {
                context.Securities.Add(security);
                context.SaveChanges();
            }

            return RedirectToAction("Detail", new { security.Id });
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var security = context.Securities.Find(id);

            if (security == null)
                return new HttpNotFoundResult();

            return View(security);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id, Ticker, Name")]Security security)
        {
            context.Securities.AddOrUpdate(security);
            context.SaveChanges();

            return RedirectToAction("Detail", new { Id = security.Id });
        }

        public ActionResult Detail(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var security = context.Securities.Find(id);

            if (security == null)
                return new HttpNotFoundResult();

            return View(security);
        }

        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                var security = context.Securities.Include(x => x.BrokerageTransactions).SingleOrDefault(x => x.Id == id);

                if (security != null)
                {
                    context.Securities.Remove(security);
                    context.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }
    }

    public class ReportsController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("CostBasis");
        }

        public ActionResult CostBasis()
        {
            var model = new CostBasisSummary
                        {
                            SecurityLots = new[]
                                           {
                                               new SecurityLot
                                               {
                                                   SecurityId = 1,
                                                   SecurityTicker = "VTSAX",
                                                   SecurityName = "Vanguard Total Stock Market Admiral Shares Fund",
                                                   Quantity = 61,
                                                   TotalCost = 18724.13m,
                                                   CurrentMarketValueDate = DateTime.Now,
                                                   CurrentMarketValue = 20132.45m,
                                                   ShortTermCapitalGain = 11.13m,
                                                   LongTermCapitalGain = 1392.76m,
                                                   TotalGainLoss = 1403.89m,
                                                   Lots = new[]
                                                          {
                                                              new Lot { AquiredDate = new DateTime(2013, 1, 3), Quantity = 11, CostPerShare = 104.60m, TotalCost = 1143.76m, CurrentMarketValue = 1200, ShortTermCapitalGain = 0, LongTermCapitalGain = 56.24m, TotalGainLoss = 56.24m, },
                                                              new Lot { AquiredDate = new DateTime(2013, 1, 4), Quantity = 11, CostPerShare = 104.60m, TotalCost = 1143.76m, CurrentMarketValue = 1200, ShortTermCapitalGain = 0, LongTermCapitalGain = 56.24m, TotalGainLoss = 56.24m, },
                                                              new Lot { AquiredDate = new DateTime(2013, 1, 5), Quantity = 11, CostPerShare = 104.60m, TotalCost = 1143.76m, CurrentMarketValue = 1200, ShortTermCapitalGain = 0, LongTermCapitalGain = 56.24m, TotalGainLoss = 56.24m, },
                                                              new Lot { AquiredDate = new DateTime(2013, 6, 3), Quantity = 11, CostPerShare = 104.60m, TotalCost = 1143.76m, CurrentMarketValue = 1200, ShortTermCapitalGain = 0, LongTermCapitalGain = 56.24m, TotalGainLoss = 56.24m, },
                                                              new Lot { AquiredDate = new DateTime(2013, 7, 13), Quantity = 11, CostPerShare = 104.60m, TotalCost = 1143.76m, CurrentMarketValue = 1200, ShortTermCapitalGain = 56.24m, LongTermCapitalGain = 0, TotalGainLoss = 56.24m, },
                                                              new Lot { AquiredDate = new DateTime(2014, 1, 1), Quantity = 11, CostPerShare = 104.60m, TotalCost = 1143.76m, CurrentMarketValue = 1200, ShortTermCapitalGain = 56.24m, LongTermCapitalGain = 0, TotalGainLoss = 56.24m, }
                                                          }
                                               },

                                               new SecurityLot
                                               {
                                                   SecurityId = 2,
                                                   SecurityTicker = "VGSLX",
                                                   SecurityName = "Vanguard REIT Admiral Shares Fund",
                                                   Quantity = 23,
                                                   TotalCost = 11926.96m,
                                                   CurrentMarketValueDate = DateTime.Now,
                                                   CurrentMarketValue = 12926.96m,
                                                   ShortTermCapitalGain = -100m,
                                                   LongTermCapitalGain = 1100m,
                                                   TotalGainLoss = 100m,
                                                   Lots = new[]
                                                          {
                                                              new Lot { AquiredDate = new DateTime(2013, 1, 3), Quantity = 11, CostPerShare = 104.60m, TotalCost = 1143.76m, CurrentMarketValue = 1200, ShortTermCapitalGain = 0, LongTermCapitalGain = 56.24m, TotalGainLoss = 56.24m, },
                                                              new Lot { AquiredDate = new DateTime(2013, 1, 4), Quantity = 11, CostPerShare = 104.60m, TotalCost = 1143.76m, CurrentMarketValue = 1200, ShortTermCapitalGain = 0, LongTermCapitalGain = 56.24m, TotalGainLoss = 56.24m, },
                                                              new Lot { AquiredDate = new DateTime(2013, 1, 5), Quantity = 11, CostPerShare = 104.60m, TotalCost = 1143.76m, CurrentMarketValue = 1200, ShortTermCapitalGain = 0, LongTermCapitalGain = 56.24m, TotalGainLoss = 56.24m, },
                                                              new Lot { AquiredDate = new DateTime(2013, 6, 3), Quantity = 11, CostPerShare = 104.60m, TotalCost = 1143.76m, CurrentMarketValue = 1200, ShortTermCapitalGain = 0, LongTermCapitalGain = 56.24m, TotalGainLoss = 56.24m, },
                                                              new Lot { AquiredDate = new DateTime(2013, 7, 13), Quantity = 11, CostPerShare = 104.60m, TotalCost = 1143.76m, CurrentMarketValue = 1200, ShortTermCapitalGain = 56.24m, LongTermCapitalGain = 0, TotalGainLoss = 56.24m, },
                                                              new Lot { AquiredDate = new DateTime(2014, 1, 1), Quantity = 11, CostPerShare = 104.60m, TotalCost = 1143.76m, CurrentMarketValue = 1200, ShortTermCapitalGain = 56.24m, LongTermCapitalGain = 0, TotalGainLoss = 56.24m, }
                                                          }
                                               },
                                           }
                        };

            return View(model);
        }
    }
}