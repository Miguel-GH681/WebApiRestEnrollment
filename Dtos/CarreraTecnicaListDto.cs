namespace WebApiKalum_net_2022.Dtos
{
    public class CarreraTecnicaListDto
    {
        public string CarreraId {get;set;}
        public string Nombre {get; set;}
        public List<AspiranteDto> Aspirantes {get;set;}
        public List<InscripcionesDto> Inscripciones {get;set;}
    }
}