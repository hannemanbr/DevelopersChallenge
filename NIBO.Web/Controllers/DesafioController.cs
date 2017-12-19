using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NIBO.DAL.Context;
using NIBO.Dominio.Infra;
using NIBO.Dominio.Util;
using NIBO.Modelo;
using NIBO.Web.Util;
using NIBO.Web.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NIBO.Web.Controllers
{
    public class DesafioController : Controller
    {
        private readonly TorneioContext _context;
        private DesafioInfra _desafioInfra = new DesafioInfra();
        private EquipeInfra _equipeInfra = new EquipeInfra();
        private EventoInfra _eventoInfra = new EventoInfra();
        private DesafioUtil _desafioUtil = new DesafioUtil();
        private EventoUtil _eventoUtil = new EventoUtil();

        public DesafioController(TorneioContext contexto)
        {
            _context = contexto;
        }

        private void GerarViewBagListaEquipes(int id)
        {
            var listEquipeView = _desafioUtil.GetEquipesByDesafio(_context, id);
            var listDesafiosView = _desafioUtil.GetDesafiosById(id, _context);

            ViewBag.equipe01 = new SelectList(listEquipeView, "Id", "Nome");
            ViewBag.equipe02 = new SelectList(listEquipeView, "Id", "Nome");
            ViewBag.DesafiosCadastrados = listDesafiosView;

            if (listDesafiosView.Count() == 0)
            {
                ViewBag.MsgRetornoDesafios = MessageUtil.SemCadastroDesafios();
            }

        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(_desafioUtil.GetEventos(_context));
        }

        public IActionResult Create(int id)
        {
            GerarViewBagListaEquipes(id);
            return View(_eventoUtil.ConversaoParaEventoViewPorId(id, _context));
        }

        [HttpPost]
        public ActionResult Create(EventoView evento, string equipe01, string equipe02)
        {
            DesafioView desafioView = new DesafioView();

            //validando
            if (equipe01 == null || equipe02 == null)
            {
                ViewBag.MsgRetorno += MessageUtil.ErrorDesafioEquipeSelecionar();
                return View(_eventoUtil.ConversaoParaEventoViewPorId(evento.Id, _context));
            }

            desafioView.IdEvento = evento.Id;
            desafioView.Nome = evento.Nome;
            desafioView.IdTime01 = Convert.ToInt32(equipe01);
            desafioView.IdTime02 = Convert.ToInt32(equipe02);

            //validaçao
            var listaValidacao = _desafioUtil.Validate(desafioView, _context);

            if (listaValidacao.Where(x => x.Resultado == false).Any())
            {
                foreach (ResultadoValidacao item in listaValidacao)
                {
                    ViewBag.MsgRetorno += item.Mensagem + " | ";
                }
            }
            else
            {

                try
                {
                    if (desafioView != null)
                    {
                        _desafioInfra.Insert(
                            _desafioUtil.ConvertDesafioViewInDesafio(desafioView)
                            , _context
                        );

                    }

                    ViewBag.MsgRetorno = MessageUtil.Sucess();


                }
                catch (System.Exception ex)
                {
                    ViewBag.MsgRetorno = MessageUtil.Error() + " - " + ex.Message;
                }
            }

            GerarViewBagListaEquipes(evento.Id);
            return View(_eventoUtil.ConversaoParaEventoViewPorId(evento.Id, _context));

        }

        public IActionResult Edit(int id)
        {

            return View(_desafioUtil.GetById(id, _context));

        }

        [HttpPost]
        public IActionResult Edit(DesafioView desafioView)
        {

            try
            {
                _desafioInfra.Update(
                    _context, _desafioUtil.ConvertDesafioViewInDesafio(desafioView)
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
            return View(_desafioUtil.GetById(id, _context));
        }

        [HttpPost]
        public IActionResult Delete(DesafioView desafioView)
        {

            try
            {
                _desafioInfra.Delete(
                    _context, _desafioUtil.ConvertDesafioViewInDesafio(desafioView)
                );

                ViewBag.MsgRetorno = MessageUtil.Sucess();

            }
            catch (System.Exception ex)
            {
                ViewBag.MsgRetorno = MessageUtil.Error() + " - " + ex.Message;
            }

            return View("Index", _desafioUtil.GetDesafios(_context));

        }
    }
}
