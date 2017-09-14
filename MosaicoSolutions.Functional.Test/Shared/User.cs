using System;

namespace MosaicoSolutions.Functional.Test.Shared
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime? LastAccess { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
}