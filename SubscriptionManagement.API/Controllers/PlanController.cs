using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SubscriptionManagement.BLL.Interfaces;
using SubscriptionManagement.Models.DTO.Plan;
using SubscriptionManagement.Models.ViewModels.Plan;

namespace SubscriptionManagement.API.Controllers
{
    public class PlanController : Controller
    {
        private readonly IPlanService _service;
        private readonly IMapper _mapper;

        public PlanController(IPlanService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var plans = await _service.GetAllPlansAsync();
            var viewModels = _mapper.Map<List<PlanListViewModel>>(plans);
            return View(viewModels);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _service.DeletePlanAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Create()
        {
            var viewModel = new PlanViewModel();
            return View(viewModel);
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            var planDto = await _service.GetPlanByIdAsync(id);
            if (planDto == null)
                return NotFound();

            // Map to ViewModel for the form
            var viewModel = _mapper.Map<PlanViewModel>(planDto);
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PlanViewModel model)
        {
            if (!ModelState.IsValid)
            {

                return View(model);
            }
            var planDto = _mapper.Map<PlanDto>(model);
            await _service.UpdatePlanAsync(planDto);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlanViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var planDto = _mapper.Map<PlanDto>(model);
            await _service.AddPlanAsync(planDto);
            return RedirectToAction(nameof(Index));
        }
    }
}
