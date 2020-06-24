using Microsoft.Research.SEAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SEALServer
{
    [JsonObject(MemberSerialization.OptIn)]
    public class FHEParams
    {
        [JsonProperty]
        public Ciphertext param1 { get; set; }
        [JsonProperty]
        public Ciphertext param2 { get; set; }
        [JsonProperty]
        public Ciphertext result { get; set; }
        [JsonProperty]
        public string operation { get; set; }

    }
}
