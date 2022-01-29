﻿using System.Collections.Generic;
using Core.Models;

namespace Core.Repository
{
    public interface IOrderReader
    {
        public Order FindByNo(int no);
        public List<Order> FindAll();
    }

    public interface IOrderWriter
    {
        public Order Add(Order order);
        public void UpdateAmount(int no, double amount);
        public void UpdateStatus(int no, string status);
    }

    public interface IOrder : IOrderReader, IOrderWriter
    {
    }
}