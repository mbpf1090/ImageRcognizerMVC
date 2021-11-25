using ImageRcognizerMVC.Models;
using ImageRcognizerMVC.Services;
using ImageRecognizerMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Microsoft.CognitiveServices.Speech;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ImageRcognizerMVC.Controllers
{
    public class UploadController : Controller
    {
        private readonly VisionService visionService;
        private readonly SpeechService speechService;

        public UploadController(VisionService visionService, SpeechService speechService)
        {
            this.visionService = visionService;
            this.speechService = speechService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(UploadModel data)
        {
            if (!ModelState.IsValid) return View(data);

            var photo = data.Photo;
            ImageAnalysis result = null;
            byte[] b = null;
            byte[] audio = null;

            try
            {
                using (BinaryReader br = new BinaryReader(photo.OpenReadStream()))
                {
                    b = br.ReadBytes((int)photo.OpenReadStream().Length);
                    result = await visionService.GetVisionInformation(b);
                }

                //var audioResult = await speechService.TextToSpeech($"This is maybe a {result.Tags.First().Name}!");
                var audioResult = await speechService.TextToSpeech($"{result.Description.Captions.First().Text ?? "No description available"}!");
                audio = audioResult.AudioData;

            } catch (Exception)
            {
                Console.WriteLine("Error");
            }

            if (result != null)
            {
                List<Face> faces = new List<Face>();
                if (result.Faces.Count > 0)
                {
                    foreach (var face in result.Faces)
                    {
                        faces.Add(new Face { Age = face.Age, Gender = face.Gender.ToString() });
                    }
                }
                VisionData model = new VisionData() {
                    Filename = data.Photo.FileName,
                    Categories = result.Categories.Select(b => b.Name).ToList(),
                    Tags = result.Tags.Select(t => t.Name).ToList(),
                    ImageBytes = b,
                    AdultScore = result.Adult.AdultScore,
                    GoreScore = result.Adult.GoreScore,
                    Audio = audio,
                    Faces = faces
            };
                
                return View("Save", model);
            }

            return View();
            
        }
    }
}
