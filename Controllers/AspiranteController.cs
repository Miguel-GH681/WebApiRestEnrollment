using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiKalum.Entities;
using WebApiKalum_net_2022.Dtos;
using WebApiKalum_net_2022.Utilities;

namespace WebApiKalum.Controllers{
    [ApiController]
    [Route("v1/KalumManagement/Aspirantes")]
    public class AspiranteController : ControllerBase{
        public AspiranteController(KalumDbContext _DbContext, ILogger<AspiranteController> _Logger, IMapper _Mapper){
            this.DbContext = _DbContext;
            this.Logger = _Logger;
            this.Mapper = _Mapper;
        }

        private readonly KalumDbContext DbContext;
        private readonly ILogger<AspiranteController> Logger;
        private readonly IMapper Mapper;
        
        [HttpPost]
        public async Task<ActionResult<Aspirante>> Post([FromBody] Aspirante value){
            Logger.LogDebug("Iniciando proceso para almacenar un registro de alumno");
            CarreraTecnica carrera = await DbContext.CarreraTecnica.FirstOrDefaultAsync(ct => ct.CarreraId == value.CarreraId);

            if(carrera == null){
                Logger.LogInformation($"No existe la carrera técnica con el id {value.CarreraId}");
                return BadRequest();
            } else{
                Jornada jornada = await DbContext.Jornada.FirstOrDefaultAsync(j => j.JornadaId == value.JornadaId);
                if(jornada == null){
                    return BadRequest();
                } else{
                    ExamenAdmision examen = await DbContext.ExamenAdmision.FirstOrDefaultAsync(ea => ea.ExamenId == value.ExamenId);
                    if(examen == null){
                        return BadRequest();
                    } else{
                        await DbContext.Aspirante.AddAsync(value);
                        await DbContext.SaveChangesAsync();
                        Logger.LogInformation($"Se ha creado el aspirante con éxito");
                        return Ok(value);
                    }
                }
            }
        }

        [HttpGet]
        [ServiceFilter(typeof(ActionFilter))]
        public async Task<ActionResult<IEnumerable<Aspirante>>> Get(){
            List<Aspirante> aspirantes = await DbContext.Aspirante.Include(a => a.Jornada).Include(a => a.CarreraTecnica).Include(a => a.ExamenAdmision).ToListAsync();
            if(aspirantes == null && aspirantes.Count == 0){
                return new NoContentResult();
            } else {
                List<AspiranteListDto> aspirantesDto = Mapper.Map<List<AspiranteListDto>>(aspirantes);
                return Ok(aspirantesDto);
            }
        }
    }
}