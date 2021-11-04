// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

namespace Microsoft.Solutions.PatientHub.CognitiveService
{
    public class TTSService
    {
        private string _subscriptionKey;
        private string _serviceRegion;

        public TTSService(string subscriptionKey, string serviceRegion)
        {
            _subscriptionKey = subscriptionKey;
            _serviceRegion = serviceRegion;
        }
     
        public async Task<byte[]> GetSpeechStreamAsync(string textForSpeech)
        {
            var config = SpeechConfig.FromSubscription(_subscriptionKey, _serviceRegion);
            config.SetSpeechSynthesisOutputFormat(SpeechSynthesisOutputFormat.Audio16Khz32KBitRateMonoMp3);
            using var synthesizer = new SpeechSynthesizer(config, null);

            using (var result = await synthesizer.SpeakTextAsync(textForSpeech))
            {
                if(result.Reason == ResultReason.SynthesizingAudioCompleted)
                {
                    return result.AudioData;
                }
            }

            return Array.Empty<Byte>();
        }
    }
}
