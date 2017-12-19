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

        //List al
        public List<EventoView> GetEventos(TorneioContext contexto, int idEvento = 0)
        {

            var lista = new List<EventoView>();
            var listaEventos = _eventoInfra.GetAll(contexto);

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

        public List<DesafioView> GetDesafios(TorneioContext contexto)
        {

            var lista = new List<DesafioView>();

            foreach (Desafio desafio in _desafioInfra.GetAll(contexto))
            {
                lista.Add(ConvertDesafioInDesafioView(desafio));
            }

            return lista;
        }

        public List<EquipeView> GetEquipesByDesafio(TorneioContext contexto, int idDesafio)
        {
            var listaEquipeSelecionadas = _desafioInfra.GetEquipesByDesafio(contexto, idDesafio);
            var lista = new List<EquipeView>();

            foreach (Equipe equipe in _equipeInfra.GetAll(contexto))
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

        public DesafioView GetById(int id, TorneioContext contexto)
        {
            var listDesafios = new List<DesafioView>();
            var desafio = _desafioInfra.GetByID(contexto, id);
            var desafioView = ConvertDesafioInDesafioView(desafio);

            //consulting equipes
            Equipe equipe1 = _equipeInfra.GetByID(contexto, desafioView.IdTime01);
            Equipe equipe2 = _equipeInfra.GetByID(contexto, desafioView.IdTime02);

            // conversao para EquipeView
            desafioView.equipe01 = _equipeUtil.ConversaoEquipe(equipe1);
            desafioView.equipe02 = _equipeUtil.ConversaoEquipe(equipe2);

            return desafioView;

        }

        public List<DesafioView> GetDesafiosById(int idEvento, TorneioContext contexto)
        {

            var listDesafios = _desafioInfra.GetDesafiosByEvento(contexto, idEvento);
            var listDesafiosView = new List<DesafioView>();

            foreach(Desafio desafio in listDesafios) {
                
                var desafioView = ConvertDesafioInDesafioView(desafio);

                //consulting equipe by Id
                Equipe equipe1 = _equipeInfra.GetByID(contexto, desafioView.IdTime01);
                Equipe equipe2 = _equipeInfra.GetByID(contexto, desafioView.IdTime02);

                // Convert equipe to EquipeView
                desafioView.equipe01 = _equipeUtil.ConversaoEquipe(equipe1);
                desafioView.equipe02 = _equipeUtil.ConversaoEquipe(equipe2);

                listDesafiosView.Add(desafioView);

            }

            return listDesafiosView;

        }

        public List<ResultadoValidacao> Validacao(DesafioView desafio, TorneioContext contexto){

            var lista = new List<ResultadoValidacao>();

            if (desafio == null) {
                lista.Add(new ResultadoValidacao { Resultado = false, Mensagem = MessageUtil.ErrorDesafioEvento() });
                return lista;
            } 

            if (desafio.IdEvento == 0) lista.Add(new ResultadoValidacao { Resultado = false, Mensagem = MessageUtil.ErrorDesafioEvento() });

            if (desafio.IdTime01==desafio.IdTime02) lista.Add(new ResultadoValidacao { Resultado = false, Mensagem = MessageUtil.ErrorDesafioEquipeIdentica() });

            var listaEquipesDesafio = _desafioInfra.GetEquipesByDesafio(contexto, desafio.Id);
            var Equipe01 = _equipeInfra.GetByID(contexto, desafio.IdTime01);
            var Equipe02 = _equipeInfra.GetByID(contexto, desafio.IdTime02);

            if (listaEquipesDesafio.Contains(Equipe01) && listaEquipesDesafio.Contains(Equipe02))
                lista.Add(new ResultadoValidacao { Resultado = false, Mensagem = MessageUtil.ErrorDesafioEquipeExistente() });
            
            return lista;
        }

        public DesafioView ConvertDesafioInDesafioView(Desafio desafio)
        {

            int IdView = 0;

            if (desafio.Id > 0) IdView = desafio.Id;

            var desafioView = new DesafioView
            {
                Id = IdView,
                Nome = desafio.Nome,
                IdEvento = desafio.IdEvento,
                IdTime01 = desafio.IdTime01,
                IdTime02 = desafio.IdTime02,
                PlacarTime01 = desafio.PlacarTime01,
                PlacarTime02 = desafio.PlacarTime02
            };

            return desafioView;

        }

        public Desafio ConvertDesafioViewInDesafio(DesafioView desafioView)
        {

            int Id = 0;

            if (desafioView.Id > 0) Id = desafioView.Id;

            var desafio = new Desafio
            {
                Id = Id,
                Nome = desafioView.Nome,
                IdEvento = desafioView.IdEvento,
                DELETED = 0,
                IdTime01 = desafioView.IdTime01,
                IdTime02 = desafioView.IdTime02,
                PlacarTime01 = desafioView.PlacarTime01,
                PlacarTime02 = desafioView.PlacarTime02
            };

            return desafio;

        }
    }
}
