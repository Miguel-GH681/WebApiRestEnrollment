namespace WebApiKalum_net_2022.Dtos{
    public class PaginacionDto<T>
    {
        public int Number {get;set;}
        public int TotalPages {get;set;}
        public bool First {get;set;}
        public bool Last {get;set;}
        public List<T> Content {get;set;}
    }
}