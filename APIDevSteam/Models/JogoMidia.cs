namespace APIDevSteam.Models
{
    public class JogoMidia
    {
        public string JogoMidiaId { get; set; }
        public string JogoId { get; set; }
        public Jogo? Jogo { get; set; }
        public string Tipo { get; set; }//triller imagem etc
        public string Url { get; set; }//url do video ou jogo

    }
}
