using System;
using NIBO.Modelo;
using NIBO.DAL.Context;
using System.Collections.Generic;

namespace NIBO.Dominio.Interface
{
    public interface iEquipe
    {
        void Inserir(Equipe equipe, TorneioContext contexto);
        void Atualizar(TorneioContext contexto, Equipe equipe);
        void Excluir(TorneioContext contexto, Equipe equipe);

        Equipe ConsultarPorId(TorneioContext contexto, int id);
        List<Equipe> ConsultarTodos(TorneioContext contexto);

    }
}
