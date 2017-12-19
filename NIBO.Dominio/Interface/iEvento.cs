using System;
using NIBO.Modelo;
using NIBO.DAL.Context;
using System.Collections.Generic;

namespace NIBO.Dominio.Interface
{
    public interface iEvento
    {
        void Insert(Evento evento, TorneioContext context);
        void Update(TorneioContext context, Evento evento);
        void Delete(TorneioContext context, Evento evento);

        Evento GetByID(TorneioContext context, int id);
        List<Evento> GetAll(TorneioContext context);

    }
}
