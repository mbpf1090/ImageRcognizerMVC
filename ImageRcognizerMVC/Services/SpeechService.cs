using Microsoft.CognitiveServices.Speech;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageRcognizerMVC.Services
{

    
    public class SpeechService
    {
        private readonly IConfiguration configuration;
        private SpeechConfig speechConfiguration;

        public SpeechService(IConfiguration configuration)
        {
            this.configuration = configuration;

            speechConfiguration = SpeechConfig.FromSubscription(configuration.GetSection("Speech").GetSection("subscriptionKey").Value, configuration.GetSection("Speech").GetSection("location").Value);
            speechConfiguration.SetSpeechSynthesisOutputFormat(SpeechSynthesisOutputFormat.Audio16Khz64KBitRateMonoMp3);
        }

        public async Task<SpeechSynthesisResult> TextToSpeech(string text)
        {
            using var synthesizer = new SpeechSynthesizer(speechConfiguration);
            var result = await synthesizer.SpeakTextAsync(text);
            return result;
        }
    }
}
