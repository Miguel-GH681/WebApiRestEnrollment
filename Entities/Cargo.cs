namespace WebApiKalum.Entities{
    public class Cargo{
        public string CargoId {get;set;}
        public string Descripcion {get;set;}
        public string Prefijo {get;set;}
        public Decimal Monto {get;set;}
        public bool GeneraMora {get;set;}
        public int PorcentajeMora {get;set;}
        public virtual List<CuentaPorCobrar> CuentasPorCobrar {get;set;}
    }
}