using Newtonsoft.Json;
using System.Net;

namespace GenerativeAITours.WebAPI
{
    public class ImageCache
    {
        internal static async Task SaveActivityImagesIfDontExistAsync(Tour tour)
        {
            foreach (var activity in tour.Days.SelectMany(x => x.Activities))
            {
                var activityName = activity.Name;
                string locationActivity = tour.Location + " " + activityName;
                if (!ActivityImageExists(locationActivity))
                {
                    await SaveActivityImageAsync(locationActivity);
                }
            }
        }

        private static bool ActivityImageExists(string activityName)
        {
            var path = $@"C:\Development\GenerativeAITours\GenerativeAITours.Solution\GenerativeAITours.MvcWebApplication\wwwroot\activities\{activityName.ToLower().Trim()}.jpg";

            return File.Exists(path);
        }

        private static async Task SaveActivityImageAsync(string activityName)
        {
            var unsplashAccessKey = "iNAJIV5KuG9mjNsVU7NULfuMQmrybkQpExkcO2e_gfU";
            var unsplashApiUrl = $"https://api.unsplash.com/search/photos?client_id={unsplashAccessKey}&page={1}&per_page={1}&orientation=landscape&query={activityName}";

            var client = new WebClient();
            var response = await client.DownloadStringTaskAsync(unsplashApiUrl);

            dynamic array = JsonConvert.DeserializeObject(response);

            List<string> urls = new List<string>();

            foreach (var result in array["results"])
            {
                urls.Add(result.urls.regular.ToString());
            }

            var imageUrl = urls[0];
            byte[] imageBytes = await client.DownloadDataTaskAsync(imageUrl);
            var path = $@"C:\Development\GenerativeAITours\GenerativeAITours.Solution\GenerativeAITours.MvcWebApplication\wwwroot\activities\{activityName.ToLower().Trim()}.jpg";

            System.IO.File.WriteAllBytes(path, imageBytes);

        }
    }
}
