﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankomatApp.Domain.Entities
{
    public class InternalTransfer
    {
        public decimal TransferAmount { get; set; }
        public long ReciepeintBankAccountNumber { get; set; }
        public string ReciepeintBankAccountName { get; set; }
    }
}
