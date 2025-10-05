using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SubscriptionManagement.BLL.Interfaces;
using SubscriptionManagement.Models.DTO.User;
using SubscriptionManagement.Models.ViewModels.User;

namespace SubscriptionManagement.API.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;

        public UserController(IUserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }


        public async Task<IActionResult> Index()
        {
            var users = await _service.GetAllUsersAsync();
            var viewModels = _mapper.Map<List<UserListViewModel>>(users);
            return View(viewModels);
        }


        public IActionResult Create()
        {
            var viewModel = new UserViewModel();
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var userDto = _mapper.Map<UserInputDTO>(model);
            await _service.AddUserAsync(userDto);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var userDto = await _service.GetUserByIdAsync(id);
            if (userDto == null)
                return NotFound();

            var viewModel = _mapper.Map<UserViewModel>(userDto);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] Guid id, UserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var userInputDTO = _mapper.Map<UserInputDTO>(model);
            await _service.UpdateUserAsync(id, userInputDTO);

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _service.DeleteUserAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
