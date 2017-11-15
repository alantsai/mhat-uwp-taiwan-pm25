using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MHAT.UWP.Taiwan.PM25.Model;

namespace MHAT.UWP.Taiwan.PM25.ViewModel
{
    public class MainPageViewModel
    {
        public List<PM25Model> PM25Result { get; set; }

        public MainPageViewModel()
        {
            PM25Result = new List<PM25Model>()
            {
                new PM25Model()
                {
                     PM25 = "10",
                     county = "臺中市"
                },
                new PM25Model()
                {
                    PM25 = "20",
                    county = "台北市"
                }
            };
        }
    }
}
