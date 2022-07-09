using System.ComponentModel.DataAnnotations;

namespace WebApiKalum_net_2022.Dtos
{
    public class CarreraTecnicaCreateDto
    {
        [StringLength(100, MinimumLength = 5, ErrorMessage = "La cantidad mínima de caracteres es {2} y máxima {1} para el campo {0}")]
        public string Nombre {get; set;}

    }
}