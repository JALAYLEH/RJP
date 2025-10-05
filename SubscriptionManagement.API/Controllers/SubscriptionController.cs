using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SubscriptionManagement.BLL.Interfaces;
using SubscriptionManagement.Models.DTO.Subscription;
using SubscriptionManagement.Models.Enums;
using SubscriptionManagement.Models.ViewModels.Subscription;

namespace SubscriptionManagement.API.Controllers
{
    public class SubscriptionController : Controller
    {
        private readonly ISubscriptionService _service;
        private readonly IUserService _userService;
        private readonly IPlanService _planService;
        private readonly IMapper _mapper;

        public SubscriptionController(IUserService userService,
        IPlanService planService, ISubscriptionService service, IMapper mapper)
        {
            _userService = userService;
            _planService = planService;
            _service = service;
            _mapper = mapper;
        }
        public async Task<IActionResult> Create()
        {
            var model = new SubscriptionViewModel
            {
                StartDate = DateTime.Today,
                Status = SubscriptionStatus.Active.ToString(),
            };
            await PopulateDropdowns(model);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubscriptionViewModel model)
        {
            if (!ModelState.IsValid)
            {

                return View(model);
            }

            var subdto = _mapper.Map<SubscriptionInputDto>(model);
            await _service.AddSubscriptionAsync(subdto);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var subscriptionDto = await _service.GetSubscriptionByIdAsync(id);
            if (subscriptionDto == null)
                return NotFound();

            var model = _mapper.Map<SubscriptionViewModel>(subscriptionDto);

            // Populate dropdowns here
            await PopulateDropdowns(model);

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, SubscriptionViewModel model)
        {
            if (id != model.Id)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var subDto = _mapper.Map<SubscriptionInputDto>(model);

            await _service.UpdateSubscriptionAsync(id, subDto);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index()
        {
            var users = await _service.GetAllSubscriptionsAsync();
            var viewModels = _mapper.Map<List<SubscriptionListViewModel>>(users);
            return View(viewModels);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _service.DeleteSubscriptionAsync(id);
            return RedirectToAction(nameof(Index));
        }
        private async Task PopulateDropdowns(SubscriptionViewModel model)
        {
            model.Users = (await _userService.GetAllUsersAsync())
                        .Select(u => new SelectListItem { Text = u.FullName, Value = u.Id.ToString() })
                        .ToList();

            model.Plans = (await _planService.GetAllPlansAsync())
                        .Select(p => new SelectListItem { Text = p.Name, Value = p.Id.ToString() })
                        .ToList();
            model.StatusList = Enum.GetValues(typeof(SubscriptionStatus))
                .Cast<SubscriptionStatus>()
                .Select(s => new SelectListItem
                {
                    Text = s.ToString(),
                    Value = s.ToString(),
                    Selected = s.ToString() == model.Status
                })
                .ToList();
        }

    }
}
