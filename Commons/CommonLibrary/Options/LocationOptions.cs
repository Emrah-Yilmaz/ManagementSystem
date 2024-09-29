using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Options
{
    public class LocationOptions
    {
        public CityInfo? City { get; set; }
        public DistrictInfo? District{ get; set; }
        public QuarterInfo? Quarter{ get; set; }
    }
    public class QuarterInfo
    {
        public string Url { get; set; }
        public int Limit { get; set; }
        public int Offset { get; set; }
    }
    public class DistrictInfo
    {
        public string Url { get; set; }
    }
    public class CityInfo
    {
        public string Url { get; set; }
    }
}
