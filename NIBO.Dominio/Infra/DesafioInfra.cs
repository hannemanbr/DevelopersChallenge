using System;
using System.Collections.Generic;
using NIBO.DAL.Data;
using NIBO.Dominio.Interface;
using NIBO.Modelo;
using NIBO.DAL.Context;

namespace NIBO.Dominio.Infra
{
    public class DesafioInfra : iDesafio
    {
        private DesafioDAL _desafioDAL = new DesafioDAL();

        public void Inserir(Desafio desafio, TorneioContext contexto)
        {
            _desafioDAL.Insert(desafio, contexto);
        }

        public void Atualizar(TorneioContext contexto, Desafio desafio)
        {
            _desafioDAL.Update(contexto, desafio);
        }

        public Desafio ConsultarPorId(TorneioContext contexto, int id)
        {
            return _desafioDAL.GetByID(contexto, id);
        }

        public List<Desafio> ConsultarTodos(TorneioContext contexto)
        {
            var lista = new List<Desafio>();
            lista = _desafioDAL.GetAll(contexto);

            return lista;
        }

        public void Excluir(TorneioContext contexto, Desafio desafio)
        {
            desafio.DELETED = 1;
            _desafioDAL.Update(contexto, desafio);
        }

        public List<Equipe> ConsultarEquipesPorDesafio(TorneioContext contexto, int idEvento)
        {
            return _desafioDAL.GetEquipeByDesafio(contexto, idEvento);

        }

        public List<Desafio> consultarDesafiosPorEvento(TorneioContext contexto, int idEvento)
        {
            return _desafioDAL.GetDesafiosByEvento(contexto, idEvento);
        }
    }
}
