using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalR.StockTicker
{
    public class Stock
    {
        private decimal _price;
        public string Symbol { get; set; }
    }
}