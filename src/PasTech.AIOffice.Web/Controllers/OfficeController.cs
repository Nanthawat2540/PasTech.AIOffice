using Microsoft.AspNetCore.Mvc;

namespace PasTech.AIOffice.Web.Controllers;

public class OfficeController : Controller
{
    public IActionResult Index() => View();
}
