using System;

namespace Ken120.Web.Mvc.Features.Home
{
    public class IndexViewModel
    {
        public string Greeting { get; }

        public IndexViewModel(
            string greeting)
        {
            if (greeting is null)
            {
                throw new ArgumentNullException(nameof(greeting));
            }

            Greeting = greeting;
        }
    }
}
