using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MHAT.UWP.Taiwan.PM25.Bll;
using MHAT.UWP.Taiwan.PM25.Model;
using Windows.Storage;

namespace MHAT.UWP.Taiwan.PM25.ViewModel
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private List<PM25Model> _allResult = new List<PM25Model>();

        public ObservableCollection<PM25Model> PM25Result { get; set; }

        public MainPageViewModel()
        {
            PM25Result = new ObservableCollection<PM25Model>();

            Filter = GetFilterText();

            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                _allResult.Add(
                    new PM25Model()
                    {
                        PM25 = "301",
                        county = "臺中市",
                        ItemUnit = "ug/m3",
                        Site = "沙鹿",
                         DataCreationDate = "2017-01-01"
                    });

                _allResult.Add(new PM25Model()
                    {
                        PM25 = "205",
                        county = "臺中市",
                        ItemUnit = "ug/m3",
                        Site = "西屯",
                         DataCreationDate = "2017-01-01"
                    });

                _allResult.Add(
                    new PM25Model()
                    {
                        PM25 = "190",
                        county = "台北市",
                        ItemUnit = "ug/m3",
                        Site = "大安",
                         DataCreationDate = "2017-01-01"
                    });

                _allResult.Add(
                   new PM25Model()
                   {
                       PM25 = "125",
                       county = "台北市2",
                       ItemUnit = "ug/m3",
                       Site = "大安",
                       DataCreationDate = "2017-01-01"
                   });

                _allResult.Add(
                   new PM25Model()
                   {
                       PM25 = "60",
                       county = "台北市3",
                       ItemUnit = "ug/m3",
                       Site = "大安",
                       DataCreationDate = "2017-01-01"
                   });

                _allResult.Add(
                   new PM25Model()
                   {
                       PM25 = "25",
                       county = "台北市4",
                       ItemUnit = "ug/m3",
                       Site = "大安",
                       DataCreationDate = "2017-01-01"
                   });

                _allResult.Add(
                   new PM25Model()
                   {
                       PM25 = "20",
                       county = "台北市5",
                       ItemUnit = "ug/m3",
                       Site = "大安",
                       DataCreationDate = "2017-01-01"
                   });

                FilterDate();
            }
            else
            {
                LoadData();
            }
        }

        private async void LoadData()
        {
            try
            {
                _allResult = await PM25ResultRespository.GetPM25ResultAsync();
                FilterDate();
                LoadingState = LoadingStates.Loaded;
            }
            catch
            {
                LoadingState = LoadingStates.Error;
            }
        }

        private string _filter;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Filter
        {
            get { return _filter; }
            set
            {
                if (value == _filter)
                    return;

                _filter = value;

                SetFilterText(value);

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Filter)));

                FilterDate();
            }
        }

        private LoadingStates _loadingState = LoadingStates.Loading;

        public LoadingStates LoadingState
        {
            get { return _loadingState; }
            set
            {
                if (value == _loadingState)
                    return;

                _loadingState = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LoadingState)));
            }
        }


        public enum LoadingStates
        {
            Loading,
            Loaded,
            Error
        }

        private void FilterDate()
        {
            if(_filter == null)
            {
                _filter = string.Empty;
            }

            var filterResult = _allResult.Where(x => x.county.Contains(Filter)).ToList();

            var removeList = PM25Result.Except(filterResult).ToList();

            foreach (var item in removeList)
            {
                PM25Result.Remove(item);
            }

            var resultCount = filterResult.Count;
            for (int i = 0; i < resultCount; i++)
            {
                var item = filterResult[i];
                if (i + 1 > PM25Result.Count || !PM25Result[i].Equals(item))
                {
                    PM25Result.Insert(i, item);
                }
            }
        }

        public void SetFilterText(string filter)
        {
            if (ApplicationData.Current.RoamingSettings.Values.ContainsKey(SettingPageViewModel.ReemberFilterSettingKey))
            {
                if ((bool)ApplicationData.Current.RoamingSettings.Values[SettingPageViewModel.ReemberFilterSettingKey])
                {
                    ApplicationData.Current.RoamingSettings.Values[nameof(Filter)] = filter;
                }
            }
        }

        public string GetFilterText()
        {
            var result = string.Empty;

            if (ApplicationData.Current.RoamingSettings.Values.ContainsKey(SettingPageViewModel.ReemberFilterSettingKey))
            {
                if ((bool)ApplicationData.Current.RoamingSettings.Values[SettingPageViewModel.ReemberFilterSettingKey])
                {
                    result = ApplicationData.Current.RoamingSettings.Values[nameof(Filter)]?.ToString() ?? string.Empty;
                }
            }

            return result;
        }
    }
}
