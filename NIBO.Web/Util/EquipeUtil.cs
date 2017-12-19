using System.Collections.Generic;
using NIBO.DAL.Context;
using NIBO.Dominio.Infra;
using NIBO.Modelo;
using NIBO.Web.ViewModels;

namespace NIBO.Web.Util
{
    public class EquipeUtil
    {
        private EquipeInfra _equipeInfra = new EquipeInfra();

        public List<EquipeView> ConsultarEquipe(TorneioContext contexto)
        {

            var lista = new List<EquipeView>();

            foreach (Equipe equipe in _equipeInfra.ConsultarTodos(contexto))
            {
                lista.Add(
                    new EquipeView { Id = equipe.Id, Nome = equipe.Nome }
                );
            }

            return lista;
        }

        public EquipeView ConsultaPorId(int id, TorneioContext contexto)
        {

            var equipe = _equipeInfra.ConsultarPorId(contexto, id);
            var equipeView = new EquipeView
            {
                Id = equipe.Id,
                Nome = equipe.Nome
            };

            return equipeView;

        }

        public Equipe ConversaoEquipeView(EquipeView equipeView)
        {

            int IdView = 0;

            if (equipeView.Id > 0) IdView = equipeView.Id;

            var equipe = new Equipe
            {
                Nome = equipeView.Nome,
                Id = IdView
            };

            return equipe;

        }

        public EquipeView ConversaoEquipe(Equipe equipe)
        {

            int Id = 0;

            if (equipe.Id > 0) Id = equipe.Id;

            var equipeView = new EquipeView
            {
                Nome = equipe.Nome,
                Id = Id
            };

            return equipeView;

        }
    }
}
