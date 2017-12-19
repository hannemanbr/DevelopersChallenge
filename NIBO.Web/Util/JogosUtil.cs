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
            var list = _desafioUtil.GetDesafiosById(idEvento, context);
            return new JogosView { Desafios =  list};
        }
    }
}
