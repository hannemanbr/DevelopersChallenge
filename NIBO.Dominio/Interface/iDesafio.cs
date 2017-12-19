using System;
using NIBO.Modelo;
using NIBO.DAL.Data;
using NIBO.DAL.Context;
using System.Collections.Generic;

namespace NIBO.Dominio.Interface
{
    public interface iDesafio
    {
        void Inserir(Desafio desafio, TorneioContext contexto);
        void Atualizar(TorneioContext contexto, Desafio desafio);
        void Excluir(TorneioContext contexto, Desafio desafio);

        Desafio ConsultarPorId(TorneioContext contexto, int id);
        List<Desafio> ConsultarTodos(TorneioContext contexto);
        List<Equipe> ConsultarEquipesPorDesafio(TorneioContext contexto, int idEvento);
        List<Desafio> consultarDesafiosPorEvento(TorneioContext contexto, int idEvento);
    }
}
