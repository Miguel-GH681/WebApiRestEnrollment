using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiKalum.Entities;
using WebApiKalum_net_2022.Dtos;
using WebApiKalum_net_2022.Utilities;

namespace WebApiKalum.Controllers{

    [ApiController]
    [Route("v1/KalumManagement/CarrerasTecnicas")]
    public class CarreraTecnicaController : ControllerBase{
               
        public CarreraTecnicaController(KalumDbContext _DbContext, ILogger<CarreraTecnicaController> _Logger, IMapper _Mapper){
            this.DbContext = _DbContext;
            this.Logger = _Logger;
            this.Mapper = _Mapper;
        }

        private readonly KalumDbContext DbContext;
        private readonly ILogger<CarreraTecnicaController> Logger;
        private readonly IMapper Mapper;

/*
        Obtener Carreras Técnicas sin paginación
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarreraTecnica>>> Get(){
            List<CarreraTecnica> carrerasTecnicas = null;
            Logger.LogDebug("Iniciando proceso de consulta de carreras tecnicas en la base de datos");
            carrerasTecnicas = await DbContext.CarreraTecnica.Include(c => c.Aspirantes).Include(c => c.Inscripciones).ToListAsync();
            if(carrerasTecnicas == null || carrerasTecnicas.Count == 0){
                Logger.LogWarning("No existe carraras tecnicas");
                return new NoContentResult();
            }

            Logger.LogInformation("Se ejecutó la petición de forma exitosa");
            List<CarreraTecnicaListDto> carrerasDto = Mapper.Map<List<CarreraTecnicaListDto>>(carrerasTecnicas);
            return Ok(carrerasDto);
        }
*/

    [HttpGet("page/{page}")]
    public async Task<ActionResult<IEnumerable<CarreraTecnicaDto>>> GetPaginacion(int page){
        var queryable = this.DbContext.CarreraTecnica.Include(ct => ct.Aspirantes)
        .Include(ct => ct.Inscripciones).AsQueryable();
        var paginacion = new HttpResponsePaginacion<CarreraTecnica>(queryable, page);
        if(paginacion.Content == null && paginacion.Content.Count == 0){
            return NoContent();
        } else{
            return Ok(paginacion);
        }
    }

        [HttpGet("{id}", Name = "GetCarreraTecnica")]
        public async Task<ActionResult<CarreraTecnica>> GetCarreraTecnica(string id){
            Logger.LogDebug("Iniciando el proceso de búsqueda con el id " + id);
            var carrera = await  DbContext.CarreraTecnica.Include(c => c.Aspirantes).FirstOrDefaultAsync(ct => ct.CarreraId == id);
            if(carrera == null){
                Logger.LogWarning("No existe la carrera técnica con el id" + id);
                return NotFound();
            }
            Logger.LogInformation("Finalizando el proceso de búsqueda de forma exitosa");
            return Ok(carrera);
        }

        [HttpPost]
        public async Task<ActionResult<CarreraTecnica>> Post([FromBody] CarreraTecnicaCreateDto value){
            Logger.LogDebug("Iniciando proceso de creación de carrera técnica");

            CarreraTecnica nuevo = Mapper.Map<CarreraTecnica>(value);

            nuevo.CarreraId = Guid.NewGuid().ToString().ToUpper();
            await DbContext.CarreraTecnica.AddAsync(nuevo);            
            await DbContext.SaveChangesAsync();
            return new CreatedAtRouteResult("GetCarreraTecnica", new{id = nuevo.CarreraId}, nuevo);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CarreraTecnica>> Delete(string id){
            CarreraTecnica carreraTecnica = await DbContext.CarreraTecnica.FirstOrDefaultAsync(ct => ct.CarreraId == id);
            if(carreraTecnica == null){
                Logger.LogWarning($"No se encontro ninguna carrera tecnica con el id {id}");
                return NotFound();
            } else{
                DbContext.CarreraTecnica.Remove(carreraTecnica);
                await DbContext.SaveChangesAsync();
                Logger.LogInformation($"Se ha eliminado la correctamente la carrera técnica con el id {id}");
                return carreraTecnica;
            }
        }

        
        [HttpPut("{id}")]
        public async Task<ActionResult<CarreraTecnica>> Put(string id, [FromBody] CarreraTecnica value){
            CarreraTecnica carrera = await DbContext.CarreraTecnica.FirstOrDefaultAsync(ct => ct.CarreraId == id);
            if(carrera == null){
                return BadRequest();
            } else{
                carrera.Nombre = value.Nombre;
                DbContext.Entry(carrera).State = EntityState.Modified;
                await DbContext.SaveChangesAsync();
                return NoContent();
            }
        }
    }
}
