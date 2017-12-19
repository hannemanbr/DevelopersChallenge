using System.Collections.Generic;
using NIBO.DAL.Context;
using NIBO.DAL.Data;
using NIBO.Dominio.Interface;
using NIBO.Modelo;

namespace NIBO.Dominio.Infra
{
    public class EquipeInfra : iEquipe
    {

        private EquipeDAL _equipeDAL = new EquipeDAL();

        public void Inserir(Equipe equipe, TorneioContext contexto)
        {
            _equipeDAL.Inserir(equipe, contexto);
        }

        public void Atualizar(TorneioContext contexto, Equipe equipe)
        {
            _equipeDAL.Atualizar(contexto, equipe);
        }

        public Equipe ConsultarPorId(TorneioContext contexto, int id)
        {
            return _equipeDAL.ConsultarPorId(contexto, id);
        }

        public List<Equipe> ConsultarTodos(TorneioContext contexto)
        {
            var lista = new List<Equipe>();
            lista = _equipeDAL.ConsultarTodos(contexto);

            return lista;
        }

        public void Excluir(TorneioContext contexto, Equipe equipe)
        {
            equipe.DELETED = 1;
            _equipeDAL.Atualizar(contexto, equipe);
        }
    }
}
