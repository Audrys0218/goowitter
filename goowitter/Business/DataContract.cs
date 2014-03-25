using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Business
{
    //class DataContract
    //{
    //}
    [DataContract]
    public class Response
    {
        [DataMember(Name = "statuses")]
        public Status[] Statuses { get; set; }
    }


    [DataContract]
    public class Status
    {
        [DataMember(Name = "coordinates")]
        //public Point Point { get; set; }
        public double[] Coordinates { get; set; }
        //geo deprecated
        //++place if needed
    }

    [DataContract]
    public class Point
    {
        // Longitude,Latitude
        [DataMember(Name = "coordinates")]
        public double[] Coordinates { get; set; }
    }
}
