using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VoiceAPI.Entities.Enums
{
    public enum TransactionTypeEnum
    {
        RECEIVE,    // Candidate receive when order is finish
        SEND,       // Admin post job, candidate apply job
        DEPOSIT,    // Admin deposit to account's wallet
        REFUNDED,   // Refund to candidate if rejected
        UNLOCK      // Unlock money for candidate when order is finish
    }
}
