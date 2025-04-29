namespace APIDevSteam.Models
{
    public class Carrinho
    {
        public Guid CarrinhoId { get; set; }
        public Guid? UsuarioId { get; set; }//nn presisa estar logado

        public DateTime? DataCriação { get; set; }//histrico de pesquisa ou que ele ja adicionou ao carrinho

        public bool? Finalizado { get; set; } //se o carrinho foi finalizado ou não
        public DateTime? DataFinalizacao { get; set; } //quando a compra for finalizada
        public decimal ValorTotal { get; set; } //valor total do carrinho

    }
}
