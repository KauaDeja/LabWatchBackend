using labware_webapi.Domains;
using Microsoft.Azure.CognitiveServices.ContentModerator;
using System.IO;
using System.Text;

namespace labware_webapi.Utils
{
    public static class Moderador
    {   
        // Credenciais necessárias para usar o recurso da Azure.
        private static readonly string SubscriptionKey = "230a4eb8db9d4bb8b8d0a2be0d9c816e";
        private static readonly string Endpoint = "https://ialabwatch.cognitiveservices.azure.com/";

        public static ContentModeratorClient Authenticate(string key, string endpoint)
        {
            ContentModeratorClient client = new ContentModeratorClient(new ApiKeyServiceClientCredentials(key));
            client.Endpoint = endpoint;

            return client;
        }

        public static bool ModerarTexto(string texto)
        {
            ContentModeratorClient clientText = Authenticate(SubscriptionKey, Endpoint);

            byte[] textBytes = Encoding.UTF8.GetBytes(texto);
            MemoryStream stream = new MemoryStream(textBytes);

            var screenResult = clientText.TextModeration.ScreenText("text/plain", stream, "", true, true, null, true);

            if(screenResult.Terms != null)
            {
                return true;
            }
            return false;

        }
    }
}
