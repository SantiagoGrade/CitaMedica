using CitaMedica.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace CitaMedica.Controllers
{
    public class PacientesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IConfiguration Configuration { get;} 
        
        public PacientesController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registrar(UsuarioModel paciente)
        {
            using (SqlConnection con = new(Configuration["ConnetionStrings:conexion"]))
            {
                using (SqlCommand cmd = new("sp_agendar", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Cedula",System.Data.SqlDbType.Int).Value = paciente.IdPaciente;
                    cmd.Parameters.Add("@Nombre", System.Data.SqlDbType.VarChar).Value = paciente.NPaciente;
                    cmd.Parameters.Add("@Area", System.Data.SqlDbType.VarChar).Value = paciente.area;
                    cmd.Parameters.Add("@Fecha", System.Data.SqlDbType.Date).Value = paciente.fecha;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return Redirect("Index");
        }

    }
}
