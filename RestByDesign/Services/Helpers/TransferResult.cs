using System.Collections.Generic;

namespace RestByDesign.Services.Helpers
{
    public class TransferResult
    {
        public TransferResult()
        {
            Errors = new List<string>();
        }

        public IEnumerable<string> Errors { get; set; }
    }
}