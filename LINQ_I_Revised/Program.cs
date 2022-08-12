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
            var data = JsonSerializer.Deserialize<List<Selic>>(json);

            var firstDateAvailable = data.Min(x => x.Date);
            var lastDateAvailable = data.Max(x => x.Date);

            Console.WriteLine($"\nDADOS DA SÉRIE HISTÓRICA DA SELIC DE " +
                  $"{firstDateAvailable.ToString("dd/MM/yyyy")} " +
                  $"A {lastDateAvailable.ToString("dd/MM/yyyy")}");
            Console.WriteLine("\n##### Panorama Geral #####\n");

            // -------------------------
            // Maior e menor valor da Selic
            var selicMin = data.Select(x => x.SelicValue).Min();
            Console.WriteLine($"Menor valor histórico: {selicMin.ToString("F2")}%");

            var selicMax = data.Select(x => x.SelicValue).Max();
            Console.WriteLine($"Maior valor histórico: {selicMax.ToString("F2")}%");

            // -------------------------
            // Valor mais comum da Selic
            var selicCountMostFrequentValues = data
                    .GroupBy(x => x.SelicValue)
                    .Select(x => x.Count())
                    .Max(x => x);

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

            // -------------------------
            // Valor médio
            var selicAverage = data.Average(x => x.SelicValue);
            Console.WriteLine($"Valor médio: {selicAverage.ToString("F2")}%");

            // -------------------------
            // Encontre os meses em que houve mudança no valor da selic
            var numberOfMonthsBetweenDates = Math.Abs(12 * (firstDateAvailable.Year - lastDateAvailable.Year)
                                                      + firstDateAvailable.Month - lastDateAvailable.Month);

            var monthsSelicValueVariation = new List<DateTime>();

            for (int i = 0; i <= numberOfMonthsBetweenDates; i++)
            {
                var currentDateFirstDayMonth = new DateTime(firstDateAvailable.Year, firstDateAvailable.Month, 1).AddMonths(i);
                var currentMonth = currentDateFirstDayMonth.Month;
                var currentYear = currentDateFirstDayMonth.Year;

                var countSelicValueVariation = data
                        .Where(x => x.Date.Month == currentMonth && x.Date.Year == currentYear)
                        .GroupBy(x => x.SelicValue)
                        .Count();

                if (countSelicValueVariation > 1)
                {
                    monthsSelicValueVariation.Add(currentDateFirstDayMonth);
                }
            }

            Console.WriteLine("\nMeses em que houve mudança na Selic: ");
            foreach (var item in monthsSelicValueVariation)
            {
                Console.WriteLine($"- {item.ToString("MM/yyyy")} ");
            }

            // -------------------------
            // Valor médio de cada trimestre a partir de 2016
            Console.WriteLine("\n--------------------------------------------");
            Console.WriteLine("##### Dados por Trimestre #####");
            var quarterFirstDay2016 = new DateTime(2016, 1, 1);
            var quartersQuantity = Quarter.GetQuarters(quarterFirstDay2016, lastDateAvailable);

            for (int i = 0; i <= quartersQuantity; i++)
            {
                var currentQuarterDateFirstDayMonth = quarterFirstDay2016.AddMonths(3 * i);
                var currentQuarter = Quarter.GetQuarterFromDate(currentQuarterDateFirstDayMonth);
                var currentYear = currentQuarterDateFirstDayMonth.Year;

                var currentQuarterStartDate = Quarter.GetQuarterStartDate(currentQuarterDateFirstDayMonth);
                var currentQuarterEndDate = Quarter.GetQuarterEndDate(currentQuarterDateFirstDayMonth);

                Console.WriteLine($"\n####   {currentQuarter}º trimestre - {currentYear}   ####");
                Console.WriteLine($"#### {currentQuarterStartDate.ToString("dd/MM/yyyy")} " +
                                  $"a {currentQuarterEndDate.ToString("dd/MM/yyyy")} ####");

                var selicAverageQuarter = data
                        .Where(x => x.Date >= currentQuarterStartDate && x.Date <= currentQuarterEndDate)
                        .Average(x => x.SelicValue);

                Console.WriteLine($"Valor médio: {selicAverageQuarter.ToString("F2")}%");
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

            var startDateSinceMar21 = new DateTime(2021, 3, 1);

            var numberOfMonthsBetweenDates21 = Math.Abs(12 * (startDateSinceMar21.Year - lastDateAvailable.Year)
                                      + startDateSinceMar21.Month - lastDateAvailable.Month);

            var firstDayMonthValues = new List<double>();
            var lastDayMonthValues = new List<double>();

            // Não considerando o mês não finalizado
            for (int i = 0; i < numberOfMonthsBetweenDates21; i++)
            {
                var currentDate = startDateSinceMar21.AddMonths(i);
                var currentYear = currentDate.Year;
                var currentMonth = currentDate.Month;

                var firstDayMonthSelicValue = data
                        .Where(x => x.Date.Month == currentMonth && x.Date.Year == currentYear)
                        .OrderBy(x => x.Date)
                        .Select(x => x.SelicValue)
                        .First();

                var lastDayMonthSelicValue = data
                        .Where(x => x.Date.Month == currentMonth && x.Date.Year == currentYear)
                        .OrderByDescending(x => x.Date)
                        .Select(x => x.SelicValue)
                        .First();

                firstDayMonthValues.Add(firstDayMonthSelicValue);
                lastDayMonthValues.Add(lastDayMonthSelicValue);
            }

            var monthRates = firstDayMonthValues
                .Zip(lastDayMonthValues, (first, second) => (second - first))
                .Average();
         
            Console.WriteLine($"\nTaxa média mensal: +{monthRates.ToString("F2")}%");
        }
    }
}
