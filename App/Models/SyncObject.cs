using System.Collections.Generic;

namespace Fabic.Core.Models
{
    public class SyncObject
    {
        public string UserID { get; set; }
        public List<IChooseChart> Charts { get; set; }
        public List<IChooseChartItem> ChartItems { get; set; }
        public List<BehaviourScale> Scales { get; set; }
        public List<BehaviourScaleItem> ScaleItems { get; set; }

        //public dynamic UserID { get; set; }
        //    public dynamic Charts { get; set; }
        //    public dynamic ChartItems { get; set; }
        //    public dynamic Scales { get; set; }
        //    public dynamic ScaleItems { get; set; }
        }
    }
