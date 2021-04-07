using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingLib
{
    public class Transaction : AccountBal
    {
        public long Sender_Acc { get; set; }
        public long Receiver_Acc { get; set; }
        public string Rec_IFSC { get; set; }
        public double Amount { get; set; }
        public DateTime Transaction_time { get; set; }
       
    }
}
