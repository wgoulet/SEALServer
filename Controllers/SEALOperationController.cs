using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Research.SEAL;
using System.IO.Pipelines;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System.Buffers;
using System.Diagnostics;

namespace SEALServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SEALOperationController : ControllerBase
    {
        private readonly ILogger<SEALOperationController> _logger;

        public SEALOperationController(ILogger<SEALOperationController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public SEALTransport Post([FromBody] SEALTransport content)
        {
            EncryptionParameters parms = new EncryptionParameters();
            byte[] ep = System.Convert.FromBase64String(content.ContextParams);
            MemoryStream mst = new MemoryStream(ep);
            parms.Load(mst);

            using SEALContext context = new SEALContext(parms);
            using Evaluator evaluator = new Evaluator(context);

            Ciphertext p1 = new Ciphertext();
            byte[] fp1 = System.Convert.FromBase64String(content.FHEParam1);
            mst = new MemoryStream(fp1);
            p1.Load(context, mst);

            Ciphertext p2 = new Ciphertext();
            byte[] fp2 = System.Convert.FromBase64String(content.FHEParam2);
            mst = new MemoryStream(fp2);
            p2.Load(context, mst);

            FHEParams fHEParams = new FHEParams();
            fHEParams.param1 = p1;
            fHEParams.param2 = p2;
            fHEParams.result = new Ciphertext();
            fHEParams.operation = content.Operation;

            if (fHEParams.operation == "add")
            {
                evaluator.Add(fHEParams.param1, fHEParams.param2, fHEParams.result);
                SEALTransport transport = new SEALTransport();
                // Save the result to a new SEALTransport object and return it
                transport.FHEResult = Utilities.SerializeSEAL(fHEParams.result);
                Debug.WriteLine(String.Format("Sending this answer{0}",transport.FHEResult));
                return transport;
            }
            else
                return new SEALTransport();
        }
    }
}
