using System;
using System.Collections.Generic;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace ImageRcognizerMVC.Services
{
    public class VisionService
    {

        private readonly IConfiguration configuration;

        public VisionService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<ImageAnalysis> GetVisionInformation(byte[] b)
        {
            // Create a client
            ComputerVisionClient client = Authenticate(configuration.GetSection("Vision").GetSection("endpoint").Value, configuration.GetSection("Vision").GetSection("subscriptionKey").Value);

            // Analyze an image to get features and other properties.
            return await AnalyzeImageUrl(client, b);
        }

        private ComputerVisionClient Authenticate(string endpoint, string key)
        {
            ComputerVisionClient client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(key)) { Endpoint = endpoint };
            return client;
        }
        private async Task<ImageAnalysis> AnalyzeImageUrl(ComputerVisionClient client, byte[] b)
        {

            // Creating a list that defines the features to be extracted from the image. 

            List<VisualFeatureTypes?> features = new List<VisualFeatureTypes?>()
                {
                    VisualFeatureTypes.Categories, VisualFeatureTypes.Description,
                    VisualFeatureTypes.Faces, VisualFeatureTypes.ImageType,
                    VisualFeatureTypes.Tags, VisualFeatureTypes.Adult,
                    VisualFeatureTypes.Color, VisualFeatureTypes.Brands,
                    VisualFeatureTypes.Objects
                };

            
            ImageAnalysis streamResult = await client.AnalyzeImageInStreamAsync(new MemoryStream(b), features);
            return streamResult;
        }
    }
}
