using ImageRecognizerMVC.Models;
using Microsoft.CognitiveServices.Speech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageRcognizerMVC.Models
{
    public class VisionData
    {
        public string Filename { get; set; }
        public List<string> Categories { get; set; }
        public List<string> Tags { get; set; }
        public byte[] ImageBytes { get; set; }
        public double AdultScore { get; set; }
        public double GoreScore { get; set; }
        public byte[] Audio { get; set; }
        public List<Face> Faces { get; set; }
    }
}
