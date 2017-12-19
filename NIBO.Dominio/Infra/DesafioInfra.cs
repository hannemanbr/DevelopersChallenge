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
            _desafioDAL.Inserir(desafio, contexto);
        }

        public void Atualizar(TorneioContext contexto, Desafio desafio)
        {
            _desafioDAL.Atualizar(contexto, desafio);
        }

        public Desafio ConsultarPorId(TorneioContext contexto, int id)
        {
            return _desafioDAL.ConsultarPorId(contexto, id);
        }

        public List<Desafio> ConsultarTodos(TorneioContext contexto)
        {
            var lista = new List<Desafio>();
            lista = _desafioDAL.ConsultarTodos(contexto);

            return lista;
        }

        public void Excluir(TorneioContext contexto, Desafio desafio)
        {
            desafio.DELETED = 1;
            _desafioDAL.Atualizar(contexto, desafio);
        }
    }
}
