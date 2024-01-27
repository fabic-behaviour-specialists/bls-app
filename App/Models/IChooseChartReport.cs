using System;
using System.Collections.Generic;

namespace Fabic.Core.Models
{
    /// <summary>
    /// The parent class that defines a particular I Choose Chart and its core properties
    /// </summary>
    public class IChooseChartReport : IChooseChart
    {
        public List<IChooseChartItemReport> Items
        {
            get; set;
        }

        public IChooseChartReport()
        {
            Migrated = false;
            UserID = "";
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            Items = new List<IChooseChartItemReport>();
        }
    }
}
