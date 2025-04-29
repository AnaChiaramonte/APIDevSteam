namespace APIDevSteam.Models
{
    public class CupomCarrinho
    {
        public Guid CupomCarrinhoId { get; set; }
        public Guid CarrinhoId { get; set; }
        public Guid CupomId { get; set; }
        public DateTime DataAplicacao { get; set; }
        public decimal DescontoAplicado { get; set; }
        public bool Ativo { get; set; } = true;

    }
}
