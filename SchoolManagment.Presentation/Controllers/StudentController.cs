using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchoolManagment.Domain.Commands;
using SchoolManagment.Presentation.ViewModels;
using SchoolManagment.Domain.Contracts;

namespace SchoolManagment.Presentation.Controllers
{
    public class StudentController : Controller
    {
        private readonly ICommandBus _bus;

        public StudentController(ICommandBus bus)
        {
            _bus = bus;
        }
        public IActionResult Index() => View();
        public IActionResult StudentBoard => View();

        public IActionResult CreateStudent() => View(new CreateStudentCommand());

        [HttpPost]
        public IActionResult CreateStudent(CreateStudentCommand studentVm)
        {
            _bus.Dispatch(studentVm);
            return View();
        }
        
    }
}
