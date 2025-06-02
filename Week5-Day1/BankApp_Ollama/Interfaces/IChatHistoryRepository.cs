using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApp.Models;

namespace BankApp.Interfaces
{
    public interface IChatHistoryRepository
    {
        void AddMessage(ChatEntry entry);
        ChatHistory GetHistory();
        void Clear();
    }
}