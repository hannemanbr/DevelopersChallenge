using System;
using NIBO.Modelo;
using NIBO.DAL.Context;
using System.Collections.Generic;

namespace NIBO.Dominio.Interface
{
    public interface iEvento
    {
        void Inserir(Evento evento, TorneioContext contexto);
        void Atualizar(TorneioContext contexto, Evento evento);
        void Excluir(TorneioContext contexto, Evento evento);

        Evento ConsultarPorId(TorneioContext contexto, int id);
        List<Evento> ConsultarTodos(TorneioContext contexto);

    }
}
