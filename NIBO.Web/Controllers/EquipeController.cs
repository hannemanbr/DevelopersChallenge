using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NIBO.DAL.Context;
using NIBO.Dominio.Infra;
using NIBO.Dominio.Util;
using NIBO.Modelo;
using NIBO.Web.ViewModels;
using NIBO.Web.Util;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NIBO.Web.Controllers
{
    public class EquipeController : Controller
    {

        private readonly TorneioContext _contexto;
        private EquipeInfra _equipeInfra = new EquipeInfra();
        private EquipeUtil _equipeUtil = new EquipeUtil();

        public EquipeController(TorneioContext contexto)
        {
            _contexto = contexto;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(_equipeUtil.ConsultarEquipe(_contexto));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(EquipeView equipeView)
        {

            try
            {
                //conversao de obje
                if (equipeView != null)
                {
                    _equipeInfra.Insert(
                        _equipeUtil.ConversaoEquipeView(equipeView)
                        , _contexto
                    );

                }

                ViewBag.MsgRetorno = MessageUtil.Sucess();


            }
            catch (System.Exception ex)
            {
                ViewBag.MsgRetorno = MessageUtil.Error() + " - " + ex.Message;
            }

            return View();

        }

        public IActionResult Edit(int id)
        {

            return View(_equipeUtil.ConsultaPorId(id, _contexto));

        }

        [HttpPost]
        public IActionResult Edit(EquipeView equipeView)
        {

            try
            {
                _equipeInfra.Update(
                    _contexto, _equipeUtil.ConversaoEquipeView(equipeView)
                );

                ViewBag.MsgRetorno = MessageUtil.Sucess();

            }
            catch (System.Exception ex)
            {
                ViewBag.MsgRetorno = MessageUtil.Error() + " - " + ex.Message;
            }

            return View();

        }

        public IActionResult Delete(int id)
        {
            return View(_equipeUtil.ConsultaPorId(id, _contexto));
        }

        [HttpPost]
        public IActionResult Delete(EquipeView equipeView)
        {

            try
            {
                _equipeInfra.Delete(
                    _contexto, _equipeUtil.ConversaoEquipeView(equipeView)
                );

                ViewBag.MsgRetorno = MessageUtil.Sucess();

            }
            catch (System.Exception ex)
            {
                ViewBag.MsgRetorno = MessageUtil.Error() + " - " + ex.Message;
            }

            return View("Index", _equipeUtil.ConsultarEquipe(_contexto));

        }
    }
}
