using System;
using NIBO.Modelo;

namespace NIBO.Web.ViewModels
{
    public class JogosView
    {
        public int Id { get; set; }
        public int PlacarTime01 { get; set; }
        public int PlacarTime02 { get; set; }
        public int Fase { get; set; }
        public Evento evento { get; set; }
        public Equipe equipe01 { get; set; }
        public Equipe equipe02 { get; set; }
    }
}
