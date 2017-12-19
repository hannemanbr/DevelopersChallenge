using System.Collections.Generic;
using System.Linq;
using NIBO.Modelo;
using NIBO.DAL.Context;

namespace NIBO.DAL.Data
{
    public class EquipeDAL
    {
        public void Inserir(Equipe equipe, TorneioContext contexto)
        {
            if (equipe != null)
            {
                contexto.Equipes.Add(equipe);
                contexto.SaveChanges();
            }
        }

        public List<Equipe> ConsultarTodos(TorneioContext contexto)
        {
            var lista = new List<Equipe>();

            if (contexto.Equipes != null) lista = contexto.Equipes.Where(x => x.DELETED == 0).ToList();

            return lista;
        }

        public Equipe ConsultarPorId(TorneioContext contexto, int id)
        {
            return contexto.Equipes.First(x => x.Id == id && x.DELETED==0);
        }

        public void Atualizar(TorneioContext contexto, Equipe equipe)
        {
            contexto.Equipes.Update(equipe);
            contexto.SaveChanges();
        }
    }
}
