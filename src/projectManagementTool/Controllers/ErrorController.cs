using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using projectManagementTool.Models;

namespace projectManagementTool.Controllers
{
	[Route("error")]
	public class ErrorController : Controller
	{
		[AllowAnonymous]
		public IActionResult Index()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
