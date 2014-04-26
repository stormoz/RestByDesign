using System;
using System.Collections.Generic;

namespace PersonalBanking.Domain.Model.Exceptions
{
    public class PersonalBankingException : Exception
    {
        public IEnumerable<string> ErrorMessages { get; private set; }

        public PersonalBankingException(params string[] messages)
        {
            ErrorMessages = messages;
        }
    }
}