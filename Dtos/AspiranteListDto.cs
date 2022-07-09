namespace WebApiKalum_net_2022.Dtos
{
    public class AspiranteListDto
    {
        public string NoExpediente {get;set;}       
        public string NombreCompleto {get;set;}
        public string Direccion {get;set;}
        public string Telefono {get;set;}
        public string Email {get;set;}
        public CarreraTecnicaCreateDto CarreraTecnica {get;set;}
        public JornadaCreateDto Jornada {get;set;}
        public ExamenAdmisionCreateDto ExamenAdmision {get;set;} 
    }
}