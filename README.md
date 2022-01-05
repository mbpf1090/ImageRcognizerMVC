# Image Recognizer

## Create Azure Services
The computer vision service is used for getting information about the image. The speech recognition is used to read out the image description.

- Login to Azure Dashboard
- Create a Resource Group for the Project
- Create a Service for Computer Vision and note down the subscription key and endpoint url
- Create a Service for Speech Recognition and note down the subscription key and location

## Setup
Enter your keys for Azure vision/speech services in "User Secrets" of Visual Code:
In Visual Studio, right-click on your project -> select "Manage User Secrets" and paste following snipped including your access keys:

```
{
  "Vision": {
    "subscriptionKey": "<YOUR-VISION_KEY>",
    "endpoint": "<YOUR-VISION-ENDPOINT-URL>"
  },
  "Speech": {
    "subscriptionKey": "<YOUR-SPEECH-KEY>",
    "location": "<YOUR-LOCATION>"
  }
}
```
If your keys are not configured, the App will throw an error when navigation to the upload page
## Contributors:
Daniel Arnauer, Pascal Sokal, Sebastian Weidele
