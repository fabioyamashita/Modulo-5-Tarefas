using System.Text.Json;

namespace LINQ_I
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var reader = new StreamReader("./selic.json");
            string json = reader.ReadToEnd();
            var data = JsonSerializer.Deserialize<List<Selic>>(json);

            var firstDateAvailable = data.Min(x => x.Date);
            var lastDateAvailable = data.Max(x => x.Date);

            var selicMax = data.OrderByDescending(x => x.SelicValue).First();

            var selicMin = data.OrderBy(x => x.SelicValue).First();

            var selicCountMostFrequentValues = data
                    .GroupBy(x => x.SelicValue)
                    .OrderByDescending(x => x.Count())
                    .Select(x => new { Days = x.Count() })
                    .First();

            var selicMostFrequentValues = data
                    .GroupBy(x => x.SelicValue)
                    .OrderByDescending(x => x.Count())
                    .Where(x => x.Count() == selicCountMostFrequentValues.Days)
                    .Select(x => new { Selic = x.Key })
                    .ToArray();

            var selicAverage = data.Average(x => x.SelicValue);

            // Dados de todo o histórico
            Console.WriteLine($"\nDADOS DA SÉRIE HISTÓRICA DA SELIC DE " +
                              $"{firstDateAvailable.ToString("dd/MM/yyyy")} " +
                              $"A {lastDateAvailable.ToString("dd/MM/yyyy")}");
            Console.WriteLine("\n##### Panorama Geral #####\n");
            Console.WriteLine($"Menor valor histórico: {selicMin.SelicValue.ToString("F2")}%");
            Console.WriteLine($"Maior valor histórico: {selicMax.SelicValue.ToString("F2")}%");
            Console.Write($"Valor mais comum: ");
            foreach (var item in selicMostFrequentValues)
            {
                Console.Write($"{item.Selic.ToString("F2")}% - ");
            }
            Console.WriteLine($"{selicCountMostFrequentValues.Days} dias");
            Console.WriteLine($"Valor médio: {selicAverage.ToString("F2")}%");

            // Dados por trimestre
            Console.WriteLine("\n##### Dados por Trimestre #####");
            var quartersQuantity = Quarter.GetQuarters(firstDateAvailable, lastDateAvailable);

            for (int i = 0; i <= quartersQuantity; i++)
            {
                var currentQuarterDate = firstDateAvailable.AddMonths(3 * i);
                var currentQuarter = Quarter.GetQuarterFromDate(currentQuarterDate);
                var currentYear = currentQuarterDate.Year;

                var currentQuarterStartDate = Quarter.GetQuarterStartDate(currentQuarterDate);
                var currentQuarterEndDate = Quarter.GetQuarterEndDate(currentQuarterDate);

                Console.WriteLine($"\n####   {currentQuarter}º trimestre - {currentYear}   ####");
                Console.WriteLine($"#### {currentQuarterStartDate.ToString("dd/MM/yyyy")} " +
                                  $"a {currentQuarterEndDate.ToString("dd/MM/yyyy")} ####");

                var selicMinQuarter = data
                        .Where(x => x.Date >= currentQuarterStartDate && x.Date <= currentQuarterEndDate)
                        .OrderBy(x => x.SelicValue)
                        .First();

                var selicMaxQuarter = data
                        .Where(x => x.Date >= currentQuarterStartDate && x.Date <= currentQuarterEndDate)
                        .OrderByDescending(x => x.SelicValue)
                        .First();

                var selicCountMostFrequentValuesQuarter = data
                        .Where(x => x.Date >= currentQuarterStartDate && x.Date <= currentQuarterEndDate)
                        .GroupBy(x => x.SelicValue)
                        .OrderByDescending(x => x.Count())
                        .Select(x => new { Days = x.Count() })
                        .First();

                var selicMostFrequentValuesQuarter = data
                        .Where(x => x.Date >= currentQuarterStartDate && x.Date <= currentQuarterEndDate)
                        .GroupBy(x => x.SelicValue)
                        .OrderByDescending(x => x.Count())
                        .Where(x => x.Count() == selicCountMostFrequentValuesQuarter.Days)
                        .Select(x => new { DadoQuarter = x.Key })
                        .ToArray();

                var selicAverageQuarter = data
                        .Where(x => x.Date >= currentQuarterStartDate && x.Date <= currentQuarterEndDate)
                        .Average(x => x.SelicValue);

                Console.WriteLine($"Menor valor: {selicMinQuarter.SelicValue.ToString("F2")}%");
                Console.WriteLine($"Maior valor: {selicMaxQuarter.SelicValue.ToString("F2")}%");
                Console.Write ($"Valor mais comum: ");
                foreach (var item in selicMostFrequentValuesQuarter)
                {
                    Console.Write($"{item.DadoQuarter.ToString("F2")}% - ");
                }
                Console.WriteLine($"{selicCountMostFrequentValuesQuarter.Days} dias");
                Console.WriteLine($"Valor médio: {selicAverageQuarter.ToString("F2")}%");
            }
        }
    }
}