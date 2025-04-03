using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcAssignment.Business.Interfaces;
using MvcAssignment.Shared.DTOs;
using MvcAssignment.Shared.Enums;
using MvcAssignment.Web.Enums;

namespace MvcAssignment.Web.Controllers
{
    public class RookiesController : Controller
    {
        private readonly IPersonService _personService;

        public RookiesController(IPersonService personService)
        {
            _personService = personService;
        }

        [Route("NashTech/[controller]/[action]")]
        public IActionResult Index()
        {
            var rookies = _personService.GetAllMembers();
            return View(rookies);
        }

        public IActionResult GetMaleRookies()
        {
            var rookies = _personService.GetMaleMembers();
            return View("Index", rookies);
        }

        public IActionResult GetOldestRookie()
        {
            try
            {
                var rookies = _personService.GetOldestMember();
                return View(rookies);
            }
            catch (KeyNotFoundException ex)
            {
                return View("RookiesNotFound", ex.Message);
            }
            catch (Exception ex)
            {
                return View("RookiesError", ex.Message);
            }
        }

        public IActionResult GetFullnames()
        {
            try
            {
                var fullnames = _personService.GetFullnames();
                return View(fullnames);
            }
            catch (KeyNotFoundException ex)
            {
                return View("RookiesNotFound", ex.Message);
            }
            catch (Exception ex)
            {
                return View("RookiesError", ex.Message);
            }
        }

        public IActionResult RedirectBasedOnOption(Option option, int year)
        {
            var actionName = option switch
            {
                Option.Equal => "GetRookiesByBirthYear",
                Option.Greater => "GetRookiesByBirthYearGreater",
                Option.Less => "GetRookiesByBirthYearLess",
                _ => "GetRookiesByBirthYear"
            };

            return RedirectToAction(actionName, new { year });
        }

        public IActionResult GetRookiesByBirthYear(int year)
        {
            var rookies = _personService.GetMembersByBirthYear(year);
            return View("Index", rookies);
        }

        public IActionResult GetRookiesByBirthYearGreater(int year)
        {
            var rookies = _personService.GetMembersByBirthYearGreater(year);
            return View("Index", rookies);
        }

        public IActionResult GetRookiesByBirthYearLess(int year)
        {
            var rookies = _personService.GetMembersByBirthYearLess(year);
            return View("Index", rookies);
        }

        public IActionResult DownloadRookiesExcel()
        {
            try
            {
                var result = _personService.WriteMembersToExcel();
                return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Rookies.xlsx");
            }
            catch (KeyNotFoundException ex)
            {
                return View("RookiesNotFound", ex.Message);
            }
            catch (Exception ex)
            {
                return View("RookiesError", ex.Message);
            }
        }

        public IActionResult CreateNewRookie()
        {
            ViewBag.GenderList = new SelectList(Enum.GetValues(typeof(Gender)));
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateNewRookie(PersonToCreateDTO person)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.GenderList = new SelectList(Enum.GetValues(typeof(Gender)));
                return View();
            }

            _personService.CreateNewMember(person);
            return RedirectToAction("Index");    
        }

        public IActionResult EditRookie(int id)
        {
            ViewBag.GenderList = new SelectList(Enum.GetValues(typeof(Gender)));
            var rookie = _personService.GetMemberById(id);
            return View(rookie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditRookie(PersonDTO person)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.GenderList = new SelectList(Enum.GetValues(typeof(Gender)));
                    return View(person);
                }

                _personService.EditMember(person);              
                return RedirectToAction("Index");
            }
            catch (KeyNotFoundException ex)
            {
                return View("RookiesNotFound", ex.Message);
            }
            catch (Exception ex)
            {
                return View("RookiesError", ex.Message);
            }
        }

        public IActionResult GetRookieDetails(int id)
        {
            try
            {
                var rookie = _personService.GetMemberById(id);
                return View(rookie);
            }
            catch (KeyNotFoundException ex)
            {
                return View("RookiesNotFound", ex.Message);
            }
            catch (Exception ex)
            {
                return View("RookiesError", ex.Message);
            }
        }

        public IActionResult DeleteRookie(int id, string name)
        {
            try
            {
                _personService.DeleteMember(id);
                var message = $"Person {name} was removed from the list successfully!";
                return View("RookiesSuccess", message);
            }
            catch (KeyNotFoundException ex)
            {
                return View("RookiesNotFound", ex.Message);
            }
            catch (Exception ex)
            {
                return View("RookiesError", ex.Message);
            }
        }
    }
}