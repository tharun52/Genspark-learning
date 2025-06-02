using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApp.Models
{
    public class ChatHistory
    {
        public List<ChatEntry> Messages { get; set; } = new();
    }
}