using System.Net.WebSockets;
using System.Text.Json;

namespace LINQ_I_Revised
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var reader = new StreamReader("./selic.json");
            string json = reader.ReadToEnd();
            var data = JsonSerializer.Deserialize<List<Selic>>(json).OrderBy(x => x.Date);

            var firstDateAvailable = data.Min(x => x.Date);
            var lastDateAvailable = data.Max(x => x.Date);

            Console.WriteLine($"\nDADOS DA SÉRIE HISTÓRICA DA SELIC DE " +
                  $"{firstDateAvailable.ToString("dd/MM/yyyy")} " +
                  $"A {lastDateAvailable.ToString("dd/MM/yyyy")}");
            Console.WriteLine("\n##### Panorama Geral #####\n");

            // ------------------------------------------------
            // Maior e menor valor da Selic
            var selicMin = data.Min(x => x.SelicValue);
            Console.WriteLine($"Menor valor histórico: {selicMin.ToString("F2")}%");

            var selicMax = data.Max(x => x.SelicValue);
            Console.WriteLine($"Maior valor histórico: {selicMax.ToString("F2")}%");

            // ------------------------------------------------
            // Valor mais comum da Selic
            var selicCountMostFrequentValues = data
                    .GroupBy(x => x.SelicValue)
                    .Max(x => x.Count());

            // Array porque 2 taxas diferentes podem ter existido o mesmo número de dias
            var selicMostFrequentValues = data
                    .GroupBy(x => x.SelicValue)
                    .OrderByDescending(x => x.Count())
                    .Where(x => x.Count() == selicCountMostFrequentValues)
                    .Select(x => new { Selic = x.Key })
                    .ToArray();

            Console.Write($"Valor mais comum: ");
            foreach (var item in selicMostFrequentValues)
            {
                Console.Write($"{item.Selic.ToString("F2")}% - ");
            }
            Console.WriteLine($"{selicCountMostFrequentValues} dias");

            //var selicMostFrequentValues2 = data
            //        .GroupBy(x => x.SelicValue)
            //        .OrderByDescending(x => x.Count())
            //        .Where(x => x.Count().Equals(data.GroupBy(x => x.SelicValue)
            //        .Max(x => x.Count())))
            //        .ToList();

            // ------------------------------------------------
            // Valor médio
            var selicAverage = data.Average(x => x.SelicValue);
            Console.WriteLine($"Valor médio: {selicAverage.ToString("F2")}%");

            // ------------------------------------------------
            // Encontre os meses em que houve mudança no valor da selic
            var changingMonths = data
                .Where((item, index) => index == 0 || item.SelicValue != data.ElementAt(index - 1).SelicValue);

            Console.WriteLine("\nMeses em que houve mudança na Selic: ");
            foreach (var item in changingMonths)
            {
                Console.WriteLine(item.Date.ToString("MM/yyyy"));
            }

            //var changingMonths2 = data
            //    .Zip(data.Skip(1), (first, second) => new { Date = second.Date, Diff = second.SelicValue - first.SelicValue })
            //    .Where(x => x.Diff > 0);

            var changingMonths2 = data.Skip(1)
                .Where(x => data.Zip(data, (first, second) => new { Diff = second.SelicValue - first.SelicValue } )
                  .Any(x => x.Diff > 0));
                  
            Console.WriteLine("\nMeses em que houve mudança na Selic: ");
            //foreach (var item in changingMonths2)
            //{
            //    Console.WriteLine(item.Date.ToString("MM/yyyy"));
            //}


            // -------------------------
            // Valor médio de cada trimestre a partir de 2016
            Console.WriteLine("\n--------------------------------------------");
            Console.WriteLine("##### Dados por Trimestre #####");
            var quarterlyAvgList = data
                .Where(x => x.Date.Year >= 2016)
                .GroupBy(x => Math.Ceiling(x.Date.Month / 3m) + "/" + x.Date.Year)
                .Select(x => new
                {
                    Quarterly = x.Key,
                    Average = x.Average(x => x.SelicValue)
                })
                .OrderBy(x => DateTime.Parse(x.Quarterly))
                .ToList();

            foreach(var quarterlyAvg in quarterlyAvgList)
            {
                Console.WriteLine($"\n####   {quarterlyAvg.Quarterly[0]}º trimestre - {quarterlyAvg.Quarterly.Substring(2,4)}   ####");
                Console.WriteLine($"Valor médio: {quarterlyAvg.Average.ToString("F2")}%");
            }

            // -------------------------
            // Valor mais alto e mais baixo da selic para cada presidente da república
            var presidentsBrazil = new List<President>();

            var president2011_2014 = new President("Dilma Roussef", new DateTime(2011, 1, 1), new DateTime(2013, 12, 31));
            var president2015_2016 = new President("Dilma Roussef", new DateTime(2014, 1, 1), new DateTime(2016, 08, 31));
            var president2016_2018 = new President("Michel Temer", new DateTime(2016, 9, 1), new DateTime(2018, 12, 31));
            var president2019_2022 = new President("Jair Bolsonaro", new DateTime(2019, 1, 1), lastDateAvailable);

            presidentsBrazil.Add(president2011_2014);
            presidentsBrazil.Add(president2015_2016);
            presidentsBrazil.Add(president2016_2018);
            presidentsBrazil.Add(president2019_2022);

            Console.WriteLine("\n--------------------------------------------");
            Console.WriteLine("## Análise por Presidente da República: ##");
            foreach (var president in presidentsBrazil)
            {
                var selicMinPresident = data
                        .Where(x => x.Date >= president.StartDate && x.Date <= president.EndDate)
                        .Select(x => x.SelicValue)
                        .Min();

                var selicMaxPresident = data
                        .Where(x => x.Date >= president.StartDate && x.Date <= president.EndDate)
                        .Select(x => x.SelicValue)
                        .Max();

                Console.WriteLine($"\n####   {president.Name}   ####");
                Console.WriteLine($"#### {president.StartDate.ToString("dd/MM/yyyy")} " +
                                  $"a {president.EndDate.ToString("dd/MM/yyyy")} ####");
                Console.WriteLine($"Menor valor: {selicMinPresident.ToString("F2")}%");
                Console.WriteLine($"Maior valor: {selicMaxPresident.ToString("F2")}%");
            }

            // -------------------------
            // Taxa média de aumento nesse desde março/21
            Console.WriteLine("\n--------------------------------------------");
            Console.WriteLine("## TAXA MÉDIA DE AUMENTO DA SELIC DESDE MAR/21 ##");
            var filteredData = data
                .Where(x => x.Date > new DateTime(2021, 3, 1))
                .DistinctBy(x => x.SelicValue)
                .Select(x => x.SelicValue);

            var AverageSelicSince21 = filteredData
                .Zip(filteredData.Skip(1), (a, b) => b - a)
                .Average();

            Console.WriteLine($"\nTaxa média mensal: +{AverageSelicSince21.ToString("F2")}%");
        }
    }
}
