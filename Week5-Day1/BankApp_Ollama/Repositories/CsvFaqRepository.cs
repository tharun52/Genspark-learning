using System.Globalization;
using BankApp.Interfaces;
using BankApp.Models;
using CsvHelper;
using CsvHelper.Configuration;

namespace BankApp.Repositories
{
    public class CsvFaqRepository : IFAQRepository
    {
        private readonly List<Faq> _faqs;

        public CsvFaqRepository(string csvFilePath)
        {
            using var reader = new StreamReader(csvFilePath);
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = args => args.Header.ToLower()
            };

            using var csv = new CsvReader(reader, config);
            _faqs = csv.GetRecords<Faq>().ToList();
        }

        public IEnumerable<Faq> GetAll() => _faqs;
    }
}
