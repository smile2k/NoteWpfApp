using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ResourcesLibrary
{
    public static class LanguageManager
    {
        public static void ChangeCulture(string culture)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
        }

    }

    //public class Resources
    //{
    //    // Các chuỗi tài nguyên từ Resources.resx sẽ được tạo tự động ở đây.
    //}

}
