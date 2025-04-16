using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace PROLAB2.Data
{
    public class JsonOkuyucu
    {
        public static dynamic JsonDosyaOku(string dosyaYolu)
        {
            try
            {
                string jsonVerisi = File.ReadAllText(dosyaYolu);
                return JsonConvert.DeserializeObject<dynamic>(jsonVerisi);
            }
            catch (Exception ex)
            {
                Console.WriteLine("JSON dosyası okunurken hata oluştu: " + ex.Message);
                return null;
            }
        }
    }
}
