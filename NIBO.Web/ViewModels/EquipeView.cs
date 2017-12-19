using System;
using System.Collections.Generic;

namespace NIBO.Web.ViewModels
{
    public class EquipeView
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<EquipeView> equipe01 { get; set; }
    }
}
