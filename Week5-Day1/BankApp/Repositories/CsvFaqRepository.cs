using System.Globalization;
using BankApp.Interfaces;
using BankApp.Models;
using CsvHelper;

namespace BankApp.Repositories
{
    public class CsvFaqRepository : IFaqRepository
    {
        private readonly string _csvPath;

        public CsvFaqRepository(string csvPath)
        {
            _csvPath = csvPath;
        }

        public Task<List<Faq>> GetFaqsAsync()
        {
            using var reader = new StreamReader(_csvPath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var faqs = csv.GetRecords<Faq>().ToList();
            return Task.FromResult(faqs);
        }
    }
}