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
        private readonly TorneioContext _contexto;
        private DesafioInfra _desafioInfra = new DesafioInfra();
        private EquipeInfra _equipeInfra = new EquipeInfra();
        private EventoInfra _eventoInfra = new EventoInfra();
        private DesafioUtil _desafioUtil = new DesafioUtil();
        private EventoUtil _eventoUtil = new EventoUtil();

        public DesafioController(TorneioContext contexto)
        {
            _contexto = contexto;
        }

        private void GerarViewBagListaEquipes(int id)
        {
            var listEquipeView = _desafioUtil.ConsultarEquipesPorDesafio(_contexto, id);
            var listDesafiosView = _desafioUtil.ConsultaDesafiosPorId(id, _contexto);

            ViewBag.equipe01 = new SelectList(listEquipeView, "Id", "Nome");
            ViewBag.equipe02 = new SelectList(listEquipeView, "Id", "Nome");
            ViewBag.DesafiosCadastrados = listDesafiosView;

            if (listDesafiosView.Count() == 0)
            {
                ViewBag.MsgRetornoDesafios = Mensagem.SemCadastroDesafios();
            }

        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(_desafioUtil.ConsultarEventos(_contexto));
        }

        public IActionResult Create(int id)
        {
            GerarViewBagListaEquipes(id);
            return View(_eventoUtil.ConversaoParaEventoViewPorId(id, _contexto));
        }

        [HttpPost]
        public ActionResult Create(EventoView evento, string equipe01, string equipe02)
        {
            DesafioView desafioView = new DesafioView();

            //validando
            if (equipe01 == null || equipe02 == null)
            {
                ViewBag.MsgRetorno += Mensagem.ErroDesafioEquipeSelecionar();
                return View(_eventoUtil.ConversaoParaEventoViewPorId(evento.Id, _contexto));
            }

            desafioView.IdEvento = evento.Id;
            desafioView.Nome = evento.Nome;
            desafioView.IdTime01 = Convert.ToInt32(equipe01);
            desafioView.IdTime02 = Convert.ToInt32(equipe02);

            //validaçao
            var listaValidacao = _desafioUtil.Validacao(desafioView, _contexto);

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
                        _desafioInfra.Inserir(
                            _desafioUtil.ConversaoDesafioView(desafioView)
                            , _contexto
                        );

                    }

                    ViewBag.MsgRetorno = Mensagem.Sucesso();


                }
                catch (System.Exception ex)
                {
                    ViewBag.MsgRetorno = Mensagem.Erro() + " - " + ex.Message;
                }
            }

            GerarViewBagListaEquipes(evento.Id);
            return View(_eventoUtil.ConversaoParaEventoViewPorId(evento.Id, _contexto));

        }

        public IActionResult Edit(int id)
        {

            return View(_desafioUtil.ConsultaPorId(id, _contexto));

        }

        [HttpPost]
        public IActionResult Edit(DesafioView desafioView)
        {

            try
            {
                _desafioInfra.Atualizar(
                    _contexto, _desafioUtil.ConversaoDesafioView(desafioView)
                );

                ViewBag.MsgRetorno = Mensagem.Sucesso();

            }
            catch (System.Exception ex)
            {
                ViewBag.MsgRetorno = Mensagem.Erro() + " - " + ex.Message;
            }

            return View();

        }

        public IActionResult Delete(int id)
        {
            return View(_desafioUtil.ConsultaPorId(id, _contexto));
        }

        [HttpPost]
        public IActionResult Delete(DesafioView desafioView)
        {

            try
            {
                _desafioInfra.Excluir(
                    _contexto, _desafioUtil.ConversaoDesafioView(desafioView)
                );

                ViewBag.MsgRetorno = Mensagem.Sucesso();

            }
            catch (System.Exception ex)
            {
                ViewBag.MsgRetorno = Mensagem.Erro() + " - " + ex.Message;
            }

            return View("Index", _desafioUtil.ConsultarDesafio(_contexto));

        }
    }
}
