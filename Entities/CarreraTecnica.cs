using System.ComponentModel.DataAnnotations;

namespace WebApiKalum.Entities
{
    public class CarreraTecnica
    {
        public string CarreraId {get;set;}
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "La cantidad mínima de caracteres es {2} y máxima {1} para el campo {0}")]
        public string Nombre {get; set;}
        public virtual List<Aspirante> Aspirantes {get;set;}
        public virtual List<Inscripcion> Inscripciones {get;set;}
        public virtual InversionCarreraTecnica Inversion {get;set;}
    }
}