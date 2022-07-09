using System.ComponentModel.DataAnnotations;

namespace WebApiKalum.Entities{
    public class Aspirante : IValidatableObject{
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string NoExpediente {get;set;}
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Apellidos {get;set;}
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Nombres {get;set;}
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Direccion {get;set;}
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Telefono {get;set;}
        [EmailAddress(ErrorMessage = "El correo electrónico no es valido")]
        public string Email {get;set;}
        public string Estatus {get;set;} = "NO ASIGNADO";
        public string CarreraId {get;set;}
        public string JornadaId {get;set;}
        public string ExamenId {get;set;}
        public virtual CarreraTecnica CarreraTecnica {get;set;}
        public virtual Jornada Jornada {get;set;}
        public virtual ExamenAdmision ExamenAdmision {get;set;}
        public virtual List<InscripcionPago> InscripcionPagos {get;set;}
        public virtual List<ResultadoExamenAdmision> Resultados {get;set;}

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext){
            //bool expedienteValid = false;
            if(!string.IsNullOrEmpty(NoExpediente)){
                if(!NoExpediente.Contains("-")){
                    yield return new ValidationResult("El número de expediente no es válido", new string[]{nameof(NoExpediente)});
                }
            }
        }
    }
}