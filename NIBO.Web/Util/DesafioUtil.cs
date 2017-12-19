using System.Collections.Generic;
using NIBO.DAL.Context;
using NIBO.Dominio.Infra;
using NIBO.Modelo;
using NIBO.Web.ViewModels;
using System.Linq;
using NIBO.Dominio.Util;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NIBO.Web.Util
{
    public class DesafioUtil
    {
        private DesafioInfra _desafioInfra = new DesafioInfra();
        private EquipeInfra _equipeInfra = new EquipeInfra();
        private EventoInfra _eventoInfra = new EventoInfra();
        private EquipeUtil _equipeUtil = new EquipeUtil();

        public List<EventoView> ConsultarEventos(TorneioContext contexto, int idEvento = 0)
        {

            var lista = new List<EventoView>();
            var listaEventos = _eventoInfra.ConsultarTodos(contexto);

            if (idEvento > 0) listaEventos = listaEventos.Where(x => x.Id == idEvento).ToList();

            foreach (Evento evento in listaEventos)
            {
                lista.Add(
                    new EventoView
                    {
                        Id = evento.Id,
                        Nome = evento.Nome
                    }
                );
            }

            return lista;
        }

        public List<DesafioView> ConsultarDesafio(TorneioContext contexto)
        {

            var lista = new List<DesafioView>();

            foreach (Desafio desafio in _desafioInfra.ConsultarTodos(contexto))
            {
                lista.Add(
                    new DesafioView
                    {
                        Id = desafio.Id,
                        //IdTime01 = ConsultarEquipes(contexto),
                        //IdTime02 = ConsultarEquipes(contexto)
                    }
                );
            }

            return lista;
        }

        public List<EquipeView> ConsultarEquipesPorDesafio(TorneioContext contexto, int idDesafio)
        {
            var listaEquipeSelecionadas = _desafioInfra.ConsultarEquipesPorDesafio(contexto, idDesafio);
            var lista = new List<EquipeView>();

            foreach (Equipe equipe in _equipeInfra.ConsultarTodos(contexto))
            {
                if (!listaEquipeSelecionadas.Contains(equipe) || listaEquipeSelecionadas.Count() == 0)
                    lista.Add(
                        new EquipeView
                        {
                            Id = equipe.Id,
                            Nome = equipe.Nome
                        }
                );
            }

            return lista;
        }

        public List<DesafioView> ConsultaPorId(int id, TorneioContext contexto)
        {
            var listDesafios = new List<DesafioView>();
            var desafio = _desafioInfra.ConsultarPorId(contexto, id);

            var desafioView = new DesafioView
            {
                Id = desafio.Id,
                IdTime01 = desafio.IdTime01,
                IdTime02 = desafio.IdTime02,
                PlacarTime01 = desafio.PlacarTime01,
                PlacarTime02 = desafio.PlacarTime02,
                IdEvento = desafio.IdEvento,
                Nome = desafio.Nome
            };

            //consulting equipes

            Equipe equipe1 = _equipeInfra.ConsultarPorId(contexto, desafioView.IdTime01);
            Equipe equipe2 = _equipeInfra.ConsultarPorId(contexto, desafioView.IdTime02);

            // conversao para EquipeView
            desafioView.equipe01 = _equipeUtil.ConversaoEquipe(equipe1);
            desafioView.equipe02 = _equipeUtil.ConversaoEquipe(equipe2);

            return listDesafios;

        }

        public List<DesafioView> ConsultaDesafiosPorId(int idEvento, TorneioContext contexto)
        {

            var listDesafios = _desafioInfra.consultarDesafiosPorEvento(contexto, idEvento);
            var listDesafiosView = new List<DesafioView>();

            foreach(Desafio desafio in listDesafios) {

                var desafioView = new DesafioView
                {
                    Id = desafio.Id,
                    IdTime01 = desafio.IdTime01,
                    IdTime02 = desafio.IdTime02,
                    PlacarTime01 = desafio.PlacarTime01,
                    PlacarTime02 = desafio.PlacarTime02,
                    IdEvento = desafio.IdEvento,
                    Nome = desafio.Nome
                };

                //consulting equipe by Id
                Equipe equipe1 = _equipeInfra.ConsultarPorId(contexto, desafioView.IdTime01);
                Equipe equipe2 = _equipeInfra.ConsultarPorId(contexto, desafioView.IdTime02);

                // Convert equipe to EquipeView
                desafioView.equipe01 = _equipeUtil.ConversaoEquipe(equipe1);
                desafioView.equipe02 = _equipeUtil.ConversaoEquipe(equipe2);

                listDesafiosView.Add(desafioView);

            }

            return listDesafiosView;

        }

        public Desafio ConversaoDesafioView(DesafioView desafioView)
        {

            int IdView = 0;

            if (desafioView.Id > 0) IdView = desafioView.Id;

            var desafio = new Desafio
            {
                Id = IdView,
                Nome=desafioView.Nome,
                IdEvento = desafioView.IdEvento,
                DELETED=0,
                IdTime01=desafioView.IdTime01,
                IdTime02=desafioView.IdTime02,
                PlacarTime01=0,
                PlacarTime02=0
            };

            return desafio;

        }

        public List<ResultadoValidacao> Validacao(DesafioView desafio, TorneioContext contexto){

            var lista = new List<ResultadoValidacao>();

            if (desafio == null) {
                lista.Add(new ResultadoValidacao { Resultado = false, Mensagem = Mensagem.ErroDesafioEvento() });
                return lista;
            } 

            if (desafio.IdEvento == 0) lista.Add(new ResultadoValidacao { Resultado = false, Mensagem = Mensagem.ErroDesafioEvento() });

            if (desafio.IdTime01==desafio.IdTime02) lista.Add(new ResultadoValidacao { Resultado = false, Mensagem = Mensagem.ErroDesafioEquipeIdentica() });

            var listaEquipesDesafio = _desafioInfra.ConsultarEquipesPorDesafio(contexto, desafio.Id);
            var Equipe01 = _equipeInfra.ConsultarPorId(contexto, desafio.IdTime01);
            var Equipe02 = _equipeInfra.ConsultarPorId(contexto, desafio.IdTime02);

            if (listaEquipesDesafio.Contains(Equipe01) && listaEquipesDesafio.Contains(Equipe02))
                lista.Add(new ResultadoValidacao { Resultado = false, Mensagem = Mensagem.ErroDesafioEquipeExistente() });
            
            return lista;
        }
    }
}
