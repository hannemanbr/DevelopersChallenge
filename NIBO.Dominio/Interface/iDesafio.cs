using System;
using NIBO.Modelo;
using NIBO.DAL.Data;
using NIBO.DAL.Context;
using System.Collections.Generic;

namespace NIBO.Dominio.Interface
{
    public interface iDesafio
    {
        void Insert(Desafio desafio, TorneioContext context);
        void Update (TorneioContext context, Desafio desafio);
        void Delete (TorneioContext context, Desafio desafio);

        Desafio GetByID(TorneioContext context, int id);
        List<Desafio> GetAll(TorneioContext context);
        List<Equipe> GetEquipesByDesafio(TorneioContext context, int idEvento);
        List<Desafio> GetDesafiosByEvento(TorneioContext context, int idEvento);
    }
}
