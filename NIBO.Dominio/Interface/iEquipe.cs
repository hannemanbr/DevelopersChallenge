using System;
using NIBO.Modelo;
using NIBO.DAL.Context;
using System.Collections.Generic;

namespace NIBO.Dominio.Interface
{
    public interface iEquipe
    {
        void Insert(Equipe equipe, TorneioContext contexto);
        void Update(TorneioContext contexto, Equipe equipe);
        void Delete(TorneioContext contexto, Equipe equipe);

        Equipe GetByID(TorneioContext contexto, int id);
        List<Equipe> GetAll(TorneioContext contexto);

    }
}
