using System;
using System.Collections.Generic;
using System.Linq;
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
        public static async Task<List<PM25Model>> GetPM25ResultAsync()
        {
            // 測試 loading 和 error state用
            //await Task.Delay(3000);
            //throw new Exception("");

            var file = await
                Package.Current.InstalledLocation.GetFileAsync(@"Assets\TestData\sample.json");

            var resultJson = await FileIO.ReadTextAsync(file);

            var strongModel = JsonConvert.DeserializeObject<List<PM25Model>>(resultJson);

            return strongModel;
        }
    }
}
