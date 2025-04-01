using Microsoft.AspNetCore.Mvc;
using MvcAssignment.Business.Interfaces;
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
            return SafeExecute(_personService.GetAll());
        }

        public IActionResult GetMaleRookies()
        {
            return SafeExecute(_personService.GetMaleMembers());
        }

        public IActionResult GetOldestRooky()
        {
            return SafeExecute(_personService.GetOldestMember());
        }

        public IActionResult GetFullnames()
        {
            return SafeExecute(_personService.GetFullnames());
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
            return SafeExecute(_personService.GetMembersByBirthYear(year));
        }

        public IActionResult GetRookiesByBirthYearGreater(int year)
        {
            return SafeExecute(_personService.GetMembersByBirthYearGreater(year));
        }

        public IActionResult GetRookiesByBirthYearLess(int year)
        {
            return SafeExecute(_personService.GetMembersByBirthYearLess(year));
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
                return View("NotFound", ex.Message);
            }
            catch (Exception ex)
            {
                return View("RookiesError", ex.Message);
            }
        }

        private ViewResult SafeExecute<T>(T result)
        {
            try
            {
                return View(result);
            }
            catch (KeyNotFoundException ex)
            {
                return View("NotFound", ex.Message);
            }
            catch (Exception ex)
            {
                return View("RookiesError", ex.Message);
            }
        }
    }
}
