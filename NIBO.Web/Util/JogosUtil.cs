using System;
using NIBO.DAL.Context;
using NIBO.Web.ViewModels;
using System.Linq;

namespace NIBO.Web.Util
{
    public class JogosUtil
    {
        private DesafioUtil _desafioUtil = new DesafioUtil();

        public JogosView GetByEvento(int idEvento, TorneioContext context)
        {
            return new JogosView { Desafios = _desafioUtil.GetDesafiosById(idEvento, context) };
        }
    }
}
