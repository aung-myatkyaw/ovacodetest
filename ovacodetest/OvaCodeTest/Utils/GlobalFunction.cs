using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace OvaCodeTest.Utils
{
    public class GlobalFunction
    {
        public static INavigation navigation;

        public static void WriteAppSetting(string key, string value)
        {
            Device.BeginInvokeOnMainThread(
            async () =>
            {
                try
                {
                    bool flag = Application.Current.Properties.ContainsKey(key);
                    if (flag)
                    {
                        Application.Current.Properties.Remove(key);
                    }
                    Application.Current.Properties.Add(key, value);
                    await Application.Current.SavePropertiesAsync();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    //await DisplayAlert("Error", ex.Message, "OK");
                }
            });
        }

        public static string ReadAppSetting(string key)
        {
            bool flag = Application.Current.Properties.ContainsKey(key);
            if (flag)
            {
                return Application.Current.Properties[key] as string;
            }
            return "";
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            try
            {
                var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
                return Encoding.UTF8.GetString(base64EncodedBytes, 0, base64EncodedBytes.Length);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return "";
        }

        public static TResult ModelConverter<TResult>(string jsonString)
        {
            JObject req_json = JObject.Parse(jsonString);
            TResult obj = JsonConvert.DeserializeObject<TResult>(req_json.ToString());
            return obj;
        }

        public static string DictionaryToString(Dictionary<string, string> dictionary)
        {
            string dictionaryString = "{";
            foreach (KeyValuePair<string, string> keyValues in dictionary)
            {
                dictionaryString += "'" + keyValues.Key + "' : '" + keyValues.Value + "', ";
            }
            return dictionaryString.TrimEnd(',', ' ') + "}";
        }
    }
}

