using BloodDonor.Mvc.Models.ViewModel;
using BloodDonor.Mvc.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BloodDonor.Mvc.Filters
{
    public class UniqueEmailFilter : IAsyncActionFilter
    {
        private readonly IBloodDonorService _bloodDonorService;

        public UniqueEmailFilter(IBloodDonorService bloodDonorService)
        {
            _bloodDonorService = bloodDonorService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if(context.ActionArguments.TryGetValue("donor", out var value) && value is BloodDonorCreateViewModel donor)
            {                
                var existingDonors = await _bloodDonorService.GetAllAsync();

                if (existingDonors.Any(d => d.Email.Equals(donor.Email, StringComparison.OrdinalIgnoreCase)))
                {
                    context.ModelState.AddModelError("Email", "Email already exists. Checked from filter.");
                }
            }
            await next();

        }
    }
}
