// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Microsoft.Solutions.PatientHub.CognitiveService.Host.Controllers
{
    [Route("CognitiveService/[controller]")]
    [ApiController]
    public class TTSController : ControllerBase
    {
        readonly private TTSService _ttsService;
        public TTSController(TTSService ttsService)
        {
            _ttsService = ttsService;
        }

        // GET: api/<TTSController>
        [HttpPost]
        [Route("GetSpeechFile")]
        public async Task<ActionResult<string>> GetSpeechFile([FromBody]string textforSpeech)
        {
            var result = await _ttsService.GetSpeechStreamAsync(textforSpeech);

            if (result.Length > 0)
            {
                return "data:audio/mp3;base64," + Convert.ToBase64String(result);
            } else
            {
                return NotFound();
            }
        }

      
    }
}
