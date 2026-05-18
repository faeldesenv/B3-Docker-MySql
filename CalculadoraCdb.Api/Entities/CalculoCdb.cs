namespace CalculadoraCdb.Api.Entities
{
    public class CalculoCdb
    {
        public int Id { get; set; }
        public decimal ValorInvestido { get; set; }
        public int Meses { get; set; }
        public decimal ValorBruto { get; set; }
        public decimal ValorLiquido { get; set; }
        public DateTime DataCalculo { get; set; }
    }
}
