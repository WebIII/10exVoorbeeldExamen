using Microsoft.AspNetCore.Mvc.Filters;
using System;
using ThePlaceToMeet.Models.Domain;

namespace ThePlaceToMeet.Filters
{
    [AttributeUsageAttribute(AttributeTargets.All, AllowMultiple = false)]
    public class KlantFilter:  ActionFilterAttribute
    {
        private readonly IKlantRepository _klantRepository;

        public KlantFilter(IKlantRepository klantRepository)
        {
            _klantRepository = klantRepository;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.ActionArguments["klant"] = _klantRepository.GetByEmail("peter@hogent.be");
            base.OnActionExecuting(context);
        }
    }
}

