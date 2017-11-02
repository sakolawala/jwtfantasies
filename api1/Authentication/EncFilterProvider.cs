using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api1.Authentication
{
    public class EncFilterProvider : IFilterProvider
    {
        public int Order
        {
            get
            {
                return -1500;
            }
        }

        public void OnProvidersExecuted(FilterProviderContext context)
        {
        }

        public void OnProvidersExecuting(FilterProviderContext context)
        {
            // remove authorize filters
            var authFilters = context.Results.Where(x =>
               x.Descriptor.Filter.GetType() == typeof(AuthorizeFilter)).ToList();
            foreach (var f in authFilters)
                context.Results.Remove(f);
        }
    }
}
