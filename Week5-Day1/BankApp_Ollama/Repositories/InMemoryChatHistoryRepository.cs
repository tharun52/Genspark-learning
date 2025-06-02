using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApp.Interfaces;
using BankApp.Models;

namespace BankApp.Repositories
{
    public class InMemoryChatHistoryRepository : IChatHistoryRepository
    {
        private readonly ChatHistory _history = new ChatHistory();

        public void AddMessage(ChatEntry entry)
        {
            _history.Messages.Add(entry);
        }

        public ChatHistory GetHistory()
        {
            return _history;
        }

        public void Clear()
        {
            _history.Messages.Clear();
        }
    }
}