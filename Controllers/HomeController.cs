using Microsoft.AspNetCore.Mvc;
using SistemaGestionEnvios.Models;
using System.Diagnostics;
using SistemaGestionEnvios.Repository.Contrato;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaGestionEnvios.Repository.Implementacion;
using System.Threading.Tasks;
using System.Reflection;

namespace SistemaGestionEnvios.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IGenericRepository<Distrito> _distritoRespository;
        private readonly IGenericRepository<PaqueteCategoria> _paqueteRespository;
        private readonly IGenericRepository<Envio> _envioRespository;
        private readonly IGenericRepository<EstadoEnvio> _estadoEnvioRespository;
        private readonly IUsuarioRepository<Usuario> _usuarioRepository;

        public HomeController(ILogger<HomeController> logger,
             IGenericRepository<Distrito> distritoRespository,
             IGenericRepository<PaqueteCategoria> paqueteRespository,
             IGenericRepository<Envio> envioRespository,
             IGenericRepository<EstadoEnvio> estadoEnvioRespository,
             IUsuarioRepository<Usuario> usuarioRepository
            )
         {
             _logger = logger;
             _distritoRespository = distritoRespository;
            _paqueteRespository = paqueteRespository;
            _envioRespository = envioRespository;
            _estadoEnvioRespository = estadoEnvioRespository;
            _usuarioRepository = usuarioRepository;
         }


        [HttpGet]public IActionResult Login()
        {            
            return View();
        }

        [HttpPost]public IActionResult Login(string username, string password)
        {            
            var acceso = _usuarioRepository.validarCredenciales(username, password);
            if (acceso)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.mensaje = "El usuario y/o la contraseña son incorrectos";
                return View();
            }            
        }

        [HttpGet]public async Task<IActionResult> Index()
        {
            List<Envio> listaEnvios = await _envioRespository.Listar();

            List<EstadoEnvio> listaEstadoEnvio = await _estadoEnvioRespository.Listar();
            ViewBag.listaDistritos = new SelectList(listaEstadoEnvio, "id_estado_envio", "descrip_estado_envio");

            return View(listaEnvios);
        }

        [HttpPost]
        public async Task<IActionResult> Index(int idEstado = 0)
        {
            Console.WriteLine(idEstado);

            List<EstadoEnvio> listaEstadoEnvio = await _estadoEnvioRespository.Listar();
            ViewBag.listaDistritos = new SelectList(listaEstadoEnvio, "id_estado_envio", "descrip_estado_envio");

            if(idEstado > 0)
            {
                List<Envio> listaEnvios = await _envioRespository.Listar();
                var enviosFiltrados = listaEnvios.Where(env => env.refEstadoEnvio.id_estado_envio == idEstado);
                return View(enviosFiltrados);
            }
            else
            {
                List<Envio> listaEnvios = await _envioRespository.Listar();
                return View(listaEnvios);
            }

        }

        [HttpGet]public async Task<IActionResult> GrabarEnvio()        
        {
            Envio nuevoEnvio = new Envio();

            var distritos = await _distritoRespository.Listar();
            ViewBag.listaDistritos = new SelectList(distritos, "id_distrito", "nombre_dist");

            var paqueteCate = await _paqueteRespository.Listar();
            ViewBag.listaPaqueteCate = new SelectList(paqueteCate, "id_paq_cate", "descrip_paq_cate");
                        
            return View(nuevoEnvio);
        }

        [HttpPost]public async Task<IActionResult> GrabarEnvio(Envio nuevoEnvio)
        {
            var distritos = await _distritoRespository.Listar();
            ViewBag.listaDistritos = new SelectList(distritos, "id_distrito", "nombre_dist");

            var paqueteCate = await _paqueteRespository.Listar();
            ViewBag.listaPaqueteCate = new SelectList(paqueteCate, "id_paq_cate", "descrip_paq_cate");

            nuevoEnvio.fecha_registro_envio = DateTime.Today.ToString("yyyy/MM/dd");
            nuevoEnvio.id_estado_envio = 1;            


            bool _resultado = await _envioRespository.Guardar(nuevoEnvio);
            if (_resultado)
                ViewBag.msjExito = "Registro exitoso";
            else
                ViewBag.msjError = "Error al registrar";
            
            return View(nuevoEnvio);
        }

        [HttpGet]public async Task<IActionResult> DetalleEnvio(int idEnvio)
        {
            var distritos = await _distritoRespository.Listar();
            ViewBag.listaDistritos = new SelectList(distritos, "id_distrito", "nombre_dist");

            var paqueteCate = await _paqueteRespository.Listar();
            ViewBag.listaPaqueteCate = new SelectList(paqueteCate, "id_paq_cate", "descrip_paq_cate");

            var estadosEnvio = await _estadoEnvioRespository.Listar();
            ViewBag.listaEstadosEnvio = new SelectList(estadosEnvio, "id_estado_envio", "descrip_estado_envio");
            
            Envio envio = _envioRespository.BuscarId(idEnvio);

            return View(envio);
        }

        [HttpGet]public async Task<IActionResult> EditarEnvio(int idEnvio)
        {
            var distritos = await _distritoRespository.Listar();
            ViewBag.listaDistritos = new SelectList(distritos, "id_distrito", "nombre_dist");
          
            var paqueteCate = await _paqueteRespository.Listar();
            ViewBag.listaPaqueteCate = new SelectList(paqueteCate, "id_paq_cate", "descrip_paq_cate");
               
            var estadosEnvio = await _estadoEnvioRespository.Listar();
            ViewBag.listaEstadosEnvio = new SelectList(estadosEnvio, "id_estado_envio", "descrip_estado_envio");

            Envio envio = _envioRespository.BuscarId(idEnvio);

            return View(envio);
        }
        
        [HttpPost]public async Task<IActionResult> EditarEnvio(Envio objEnvio)
        {
            var distritos = await _distritoRespository.Listar();
            ViewBag.listaDistritos = new SelectList(distritos, "id_distrito", "nombre_dist");

            var paqueteCate = await _paqueteRespository.Listar();
            ViewBag.listaPaqueteCate = new SelectList(paqueteCate, "id_paq_cate", "descrip_paq_cate");

            var estadosEnvio = await _estadoEnvioRespository.Listar();
            ViewBag.listaEstadosEnvio = new SelectList(estadosEnvio, "id_estado_envio", "descrip_estado_envio");

            Console.WriteLine("desde controller");            

            bool _resultado = await _envioRespository.Editar(objEnvio);
            if (_resultado)
                ViewBag.msjExito = "Cambios guardados exitosamente";
            else
                ViewBag.msjError = "Error al guardar los cambios";
            
            return View(objEnvio);
        }

        public async Task<IActionResult> EliminarEnvio(int idEnvio)
        {
             bool _resultado = await _envioRespository.Eliminar(idEnvio);

            if (_resultado)
            {
                ViewBag.msjExito = "Registro eliminado exitosamente";
            }
            else
            {
                ViewBag.msjError = "Error al eliminar el registro";
            }
            return RedirectToAction("Index");
        }



        [HttpGet]
        public async Task<IActionResult> listarDistritos()
        {
            List<Distrito> _lista = await _distritoRespository.Listar();

            return StatusCode(StatusCodes.Status200OK, _lista);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
