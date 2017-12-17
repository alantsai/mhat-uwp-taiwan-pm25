using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace MHAT.UWP.Taiwan.PM25.Model
{
    public class PM25Model
    {
        public string Site { get; set; }
        public string county { get; set; }
        public string PM25 { get; set; }
        public string DataCreationDate { get; set; }
        public string ItemUnit { get; set; }

        public string PM25WithUnit { get { return $"{PM25} {ItemUnit}"; } }

        public int PM25Number { get { int.TryParse(PM25, out int number); return number; } }
        public Style ConditionTextColorStyle
        {
            get
            {
                if (PM25Number < 201)
                {
                    return (Style)Application.Current.Resources["blackColor"];
                }
                else
                {
                    return (Style)Application.Current.Resources["whiteColor"];
                }
            }
        }

        public Style ConditionBackgroundColorStyle
        {
            get
            {
                if (PM25Number < 51)
                {
                    return (Style)Application.Current.Resources["conditionGood"];
                }
                else
                if (PM25Number > 50 && PM25Number < 101)
                {
                    return (Style)Application.Current.Resources["conditionNormal"];
                }
                else
                if (PM25Number > 100 && PM25Number < 151)
                {
                    return (Style)Application.Current.Resources["conditionAlgeryUnHealthy"];
                }
                else
                if (PM25Number > 150 && PM25Number < 201)
                {
                    return (Style)Application.Current.Resources["conditionUnHealthy"];
                }
                else if (PM25Number > 200 && PM25Number < 301)
                {
                    return (Style)Application.Current.Resources["conditionNotHealthy"];
                }
                else
                {
                    return (Style)Application.Current.Resources["conditionDanger"];
                }
            }
        }

    }
}
