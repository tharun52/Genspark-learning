﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentApplication.Interfaces
{
    public interface IRepository<K, T> where T : class
    {
        T Add(T item);

        ICollection<T> GetAll();
    }
}
