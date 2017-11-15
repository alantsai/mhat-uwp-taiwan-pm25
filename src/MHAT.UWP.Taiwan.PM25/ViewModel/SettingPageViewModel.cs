using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace MHAT.UWP.Taiwan.PM25.ViewModel
{
    public class SettingPageViewModel : INotifyPropertyChanged
    {
        public SettingPageViewModel()
        {
            if (ApplicationData.Current.RoamingSettings.Values.ContainsKey(ReemberFilterSettingKey))
            {
                RemberFilter = (bool)ApplicationData.Current.RoamingSettings.Values[ReemberFilterSettingKey];
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private bool _isRememberFilter;

        public bool RemberFilter
        {
            get { return _isRememberFilter; }
            set
            {
                if (value == _isRememberFilter)
                    return;

                _isRememberFilter = value;

                ApplicationData.Current.RoamingSettings.Values[ReemberFilterSettingKey] = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(ReemberFilterSettingKey));
            }
        }

        public static string ReemberFilterSettingKey {get{ return nameof(RemberFilter); } }
    }
}
