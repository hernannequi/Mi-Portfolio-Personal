using System.Diagnostics;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Portfolio_Personal.Models;
using Portfolio_Personal.Services;
using Portfolio_Personal.Models.ViewModels;

namespace Portfolio_Personal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ProyectoService _proyectoService;
        private readonly ExperienciaLaboralService _experienciaLaboralService;
        private readonly FormacionAcademicaService _formacionAcademicaService;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration,
                                ProyectoService proyectoService,
                                ExperienciaLaboralService experienciaLaboralService,
                                FormacionAcademicaService formacionAcademicaService)
        {
            _logger = logger;
            _configuration = configuration;
            _proyectoService = proyectoService;
            _experienciaLaboralService = experienciaLaboralService;
            _formacionAcademicaService = formacionAcademicaService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            HomeViewModel viewModel = new HomeViewModel
            {
                Contacto = new FormularioContacto(),
                Proyectos = _proyectoService.ObtenerProyectos(),
                ExperienciasLaborales = _experienciaLaboralService.ObtenerExperienciasLaborales(),
                FormacionesAcademicas = _formacionAcademicaService.ObtenerFormacionesAcademicas()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EnviarContacto(HomeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Proyectos = _proyectoService.ObtenerProyectos();
                model.ExperienciasLaborales = _experienciaLaboralService.ObtenerExperienciasLaborales();
                model.FormacionesAcademicas = _formacionAcademicaService.ObtenerFormacionesAcademicas();

                TempData["Error"] = "Completa correctamente los campos.";

                return View("Index", model);
            }

            try
            {
                string email = _configuration["EmailSettings:Email"]!;
                string password = _configuration["EmailSettings:Password"]!;

                MailMessage mail = new MailMessage();

                mail.From = new MailAddress(email);

                mail.To.Add(email);

                mail.Subject = $"Nuevo contacto de {model.Contacto.Nombre}";

                mail.Body = $"""
        Nombre: {model.Contacto.Nombre}

        Email: {model.Contacto.Email}

        Mensaje:
        {model.Contacto.Mensaje}
        """;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(email, password),
                    EnableSsl = true
                };

                await smtp.SendMailAsync(mail);

                TempData["Success"] = "Mensaje enviado correctamente.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error: {ex.Message}";
            }

            return RedirectToAction("Index");
        }
      
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
