using System;
using System.Collections.Generic;

namespace NIBO.Web.ViewModels
{
    public class DesafioView
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int IdEvento { get; set; }
        public int IdTime01 { get; set; }
        public int IdTime02 { get; set; }
        public int PlacarTime01 { get; set; }
        public int PlacarTime02 { get; set; }
        public EquipeView equipe01 { get; set; }
        public EquipeView equipe02 { get; set; }
        public int Fase { get; set; }
    }
}
