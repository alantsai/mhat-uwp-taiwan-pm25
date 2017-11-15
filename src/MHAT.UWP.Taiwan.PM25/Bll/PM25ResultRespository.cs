using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MHAT.UWP.Taiwan.PM25.Model;
using Newtonsoft.Json;
using Windows.ApplicationModel;
using Windows.Storage;

namespace MHAT.UWP.Taiwan.PM25.Bll
{
    public class PM25ResultRespository
    {
        private static List<PM25Model> CacheData;

        public static async Task<List<PM25Model>> GetPM25ResultAsync()
        {
            // 測試 loading 和 error state用
            //await Task.Delay(3000);
            //throw new Exception("");

            if(CacheData != null && CacheData.Count > 0)
            {
                var first = CacheData.First();

                var date = DateTime.ParseExact(first.DataCreationDate, "yyyy-MM-dd hh:mm", CultureInfo.InvariantCulture);

                // 每小時更新 - 所以如果小時比現在小，表示有更新
                if(date.Day == DateTime.Now.Day && date.Hour < DateTime.Now.Hour)
                {
                    CacheData = null;
                }
            }

            if (CacheData == null)
            {
                var client = new HttpClient();
                var response = await client.GetAsync("http://opendata.epa.gov.tw/ws/Data/ATM00625/?$format=json");
                var result = await response.Content.ReadAsStringAsync();

                var strongModel = JsonConvert.DeserializeObject<List<PM25Model>>(result);

                CacheData = strongModel;
            }

            return CacheData;
        }
    }
}
