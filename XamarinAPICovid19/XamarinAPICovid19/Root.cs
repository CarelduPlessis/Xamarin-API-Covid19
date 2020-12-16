using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinAPICovid19
{
    class Root
    {
        public bool error { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public Data data { get; set; }
    }
}
