using System;

namespace LoteriaWeb.Models
{
    public class Aposta
    {
        public int Id { get; set; }
        public string NomeJogador { get; set; }
        public string NumerosSelecionados { get; set; }
        public DateTime DataAposta { get; set; }
    }
}
