using System.Collections.Generic;
using System.Linq;
using NIBO.Modelo;
using NIBO.DAL.Context;

namespace NIBO.DAL.Data
{
    public class EquipeDAL
    {
        public void Insert(Equipe equipe, TorneioContext context)
        {
            if (equipe != null)
            {
                context.Equipes.Add(equipe);
                context.SaveChanges();
            }
        }

        public List<Equipe> GetAll(TorneioContext context)
        {
            var lista = new List<Equipe>();

            if (context.Equipes != null) lista = context.Equipes.Where(x => x.DELETED == 0).ToList();

            return lista;
        }

        public Equipe GetByID(TorneioContext context, int id)
        {
            return context.Equipes.First(x => x.Id == id && x.DELETED==0);
        }

        public void Update(TorneioContext context, Equipe equipe)
        {
            context.Equipes.Update(equipe);
            context.SaveChanges();
        }
    }
}
