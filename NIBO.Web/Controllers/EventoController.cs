using Microsoft.AspNetCore.Mvc;
using NIBO.Web.ViewModels;
using System.Collections.Generic;
using NIBO.Dominio.Infra;
using NIBO.Modelo;
using NIBO.DAL.Context;
using NIBO.Dominio.Util;
using NIBO.Web.Util;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NIBO.Web.Controllers
{
    public class EventoController : Controller
    {
        private readonly TorneioContext _contexto;
        private EventoInfra _eventoInfra = new EventoInfra();
        private EventoUtil _eventoUtil = new EventoUtil();

        public EventoController(TorneioContext contexto)
        {
            _contexto = contexto;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(_eventoUtil.ConsultarEventos(_contexto));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(EventoView eventoView)
        {

            try
            {
                //conversao de obje
                if (eventoView != null)
                {
                    _eventoInfra.Insert(
                        _eventoUtil.ConversaoParaEvento(eventoView)
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

            return View(_eventoUtil.ConsultaPorId(id, _contexto));

        }

        [HttpPost]
        public IActionResult Edit(EventoView eventoView)
        {

            try
            {
                _eventoInfra.Update(
                    _contexto, _eventoUtil.ConversaoParaEvento(eventoView)
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
            return View(_eventoUtil.ConsultaPorId(id, _contexto));
        }

        [HttpPost]
        public IActionResult Delete(EventoView eventoView)
        {

            try
            {
                _eventoInfra.Delete(
                    _contexto, _eventoUtil.ConversaoParaEvento(eventoView)
                );

                ViewBag.MsgRetorno = MessageUtil.Sucess();

            }
            catch (System.Exception ex)
            {
                ViewBag.MsgRetorno = MessageUtil.Error() + " - " + ex.Message;
            }

            return View("Index", _eventoUtil.ConsultarEventos(_contexto));

        }
    }
}