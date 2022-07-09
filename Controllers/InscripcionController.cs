using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using WebApiKalum;
using WebApiKalum.Entities;
using WebApiKalum_net_2022.Dtos;

namespace WebApiKalum_net_2022.Controllers
{
    [ApiController]
    [Route("v1/KalumManagement/Inscripcion")]
    public class InscripcionController : ControllerBase
    {
        private readonly KalumDbContext DbContext;
        private readonly ILogger<InscripcionController> Logger;
        public InscripcionController(ILogger<InscripcionController> _logger, KalumDbContext _DbContext){
            this.Logger = _logger;
            this.DbContext = _DbContext;
        }

        [HttpPost("Enrollments")]
        public async Task<ActionResult<ResponseEnrollmentDto>> EnrollmentCreateAsync([FromBody] EnrollmentDto value){            
            Aspirante aspirante = await DbContext.Aspirante.FirstOrDefaultAsync(a => a.NoExpediente == value.NoExpediente);
            if(aspirante == null){
                return NoContent();
            } else{
                CarreraTecnica carrera = await DbContext.CarreraTecnica.FirstOrDefaultAsync(c => c.CarreraId == value.CarreraId);
                if(carrera == null){
                    return NoContent();
                } else{
                    bool respuesta = await CrearSolicitudAsync(value);
                    if(respuesta == true){
                        ResponseEnrollmentDto response = new ResponseEnrollmentDto();
                        response.HttpStatus = 201;
                        response.Message = "El proceso de inscripción se ha realizado con éxito";
                        return Ok(response);
                    } else{
                        return BadRequest();
                    }
                }
            }
        }

        public async Task<bool> CrearSolicitudAsync(EnrollmentDto value){
            bool proceso = false;
            ConnectionFactory factory = new ConnectionFactory();
            IConnection conexion = null;
            IModel channel = null;

            factory.UserName = "guest";
            factory.Password = "guest";
            factory.Port = 5672;
            factory.VirtualHost = "/";
            factory.HostName = "localhost";
            try{
                conexion = factory.CreateConnection();
                channel = conexion.CreateModel();
                channel.BasicPublish("kalum.queue.fanout", "", null, Encoding.UTF8.GetBytes(JsonSerializer.Serialize(value)));
                proceso = true;
            } catch(Exception e){
                Logger.LogError(e.Message);
            } finally{
                channel.Close();
                conexion.Close();
            }

            return proceso;
        }
    }
}