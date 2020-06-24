using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEALServer
{
    public class SEALTransport
    {
        public string Operation { get; set; }
        public string FHEParam1 { get; set; }
        public string FHEParam2 { get; set; }
        public string FHEResult { get; set; }
        public string ContextParams { get; set; }
    }
}
