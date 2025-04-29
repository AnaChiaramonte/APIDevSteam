namespace APIDevSteam.Models
{
    public class ItemCarrinho
    {
        public Guid ItemCarrinhoId { get; set; }
        public Guid? CarrinhoId { get; set; } //nn presisa estar logado
        public Guid? JogoId { get; set; } //nn presisa estar logado
        public int Quantidade { get; set; } //quantidade de jogos no carrinho
        public decimal ValorUnitario { get; set; } //valor unitario do jogo
        public decimal ValorTotal { get; set; } //valor total do carrinho
    }
}
