using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinAPICovid19
{
    class Data
    {
        public int recovered { get; set; }
        public int deaths { get; set; }
        public int confirmed { get; set; }
        public DateTime lastChecked { get; set; }
        public DateTime lastReported { get; set; }
        public string location { get; set; }
    }
}
