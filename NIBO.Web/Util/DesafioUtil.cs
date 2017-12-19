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
        public List<EventoView> GetEventos(TorneioContext context, int idEvento = 0)
        {

            var lista = new List<EventoView>();
            var listaEventos = _eventoInfra.GetAll(context);

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

        public List<DesafioView> GetDesafios(TorneioContext context)
        {

            var lista = new List<DesafioView>();

            foreach (Desafio desafio in _desafioInfra.GetAll(context))
            {
                lista.Add(ConvertDesafioInDesafioView(desafio));
            }

            return lista;
        }

        public List<EquipeView> GetEquipesByDesafio(TorneioContext context, int idDesafio)
        {
            var listaEquipeSelecionadas = _desafioInfra.GetEquipesByDesafio(context, idDesafio);
            var lista = new List<EquipeView>();

            foreach (Equipe equipe in _equipeInfra.GetAll(context))
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

        public DesafioView GetById(int id, TorneioContext context)
        {
            var listDesafios = new List<DesafioView>();
            var desafio = _desafioInfra.GetByID(context, id);
            var desafioView = ConvertDesafioInDesafioView(desafio);

            //consulting equipes
            Equipe equipe1 = _equipeInfra.GetByID(context, desafioView.IdTime01);
            Equipe equipe2 = _equipeInfra.GetByID(context, desafioView.IdTime02);

            // conversao para EquipeView
            desafioView.equipe01 = _equipeUtil.ConversaoEquipe(equipe1);
            desafioView.equipe02 = _equipeUtil.ConversaoEquipe(equipe2);

            return desafioView;

        }

        public List<DesafioView> GetDesafiosById(int idEvento, TorneioContext context)
        {

            var listDesafios = _desafioInfra.GetDesafiosByEvento(context, idEvento);
            var listDesafiosView = new List<DesafioView>();

            foreach(Desafio desafio in listDesafios) {
                
                var desafioView = ConvertDesafioInDesafioView(desafio);

                //consulting equipe by Id
                Equipe equipe1 = _equipeInfra.GetByID(context, desafioView.IdTime01);
                Equipe equipe2 = _equipeInfra.GetByID(context, desafioView.IdTime02);

                // Convert equipe to EquipeView
                desafioView.equipe01 = _equipeUtil.ConversaoEquipe(equipe1);
                desafioView.equipe02 = _equipeUtil.ConversaoEquipe(equipe2);

                listDesafiosView.Add(desafioView);

            }

            return listDesafiosView;

        }

        public List<ResultadoValidacao> Validate(DesafioView desafio, TorneioContext context){

            var lista = new List<ResultadoValidacao>();

            if (desafio == null) {
                lista.Add(new ResultadoValidacao { Resultado = false, Mensagem = MessageUtil.ErrorDesafioEvento() });
                return lista;
            } 

            if (desafio.IdEvento == 0) lista.Add(new ResultadoValidacao { Resultado = false, Mensagem = MessageUtil.ErrorDesafioEvento() });

            if (desafio.IdTime01==desafio.IdTime02) lista.Add(new ResultadoValidacao { Resultado = false, Mensagem = MessageUtil.ErrorDesafioEquipeIdentica() });

            var listaEquipesDesafio = _desafioInfra.GetEquipesByDesafio(context, desafio.Id);
            var Equipe01 = _equipeInfra.GetByID(context, desafio.IdTime01);
            var Equipe02 = _equipeInfra.GetByID(context, desafio.IdTime02);

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
