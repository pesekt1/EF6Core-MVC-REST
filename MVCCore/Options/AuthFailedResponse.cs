using System.Collections.Generic;

namespace MVCCore.Options
{
    public class AuthFailedResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
