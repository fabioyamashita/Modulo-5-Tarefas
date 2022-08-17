using System.Text.Json;

namespace ConsumingValorantAPI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            getHttpAsync().GetAwaiter().GetResult();
        }

        static async Task getHttpAsync()
        {
            var httpClient = new HttpClient();

            try
            {
                var response = await httpClient.GetAsync("https://valorant-api.com/v1/weapons/skinlevels");
                //var code = response.StatusCode;
                var message = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<SkinLevels>(message);

                Console.WriteLine($"Status = {result.Status}");

                int count = 1;
                foreach (var skinLevelDetail in result.Data)
                {
                    Console.WriteLine($"\n# {count}");
                    Console.WriteLine($"UUID: {skinLevelDetail.Uuid}");
                    Console.WriteLine($"Display Name: {skinLevelDetail.DisplayName}");
                    count++;
                }

                var reconWeapons = result.Data
                    .Count(x => !string.IsNullOrEmpty(x.DisplayName) && x.DisplayName.ToUpper().Contains("RECON"));
                Console.WriteLine($"Total Recon Weapons: {reconWeapons}");
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                throw;
            }
        }
    }
}