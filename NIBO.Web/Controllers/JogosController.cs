using System;
using System.Collections.Generic;
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
    public class JogosController : Controller
    {
        private readonly TorneioContext _contexto;
        private DesafioInfra _desafioInfra = new DesafioInfra();
        private EquipeInfra _equipeInfra = new EquipeInfra();
        private EventoInfra _eventoInfra = new EventoInfra();
        private DesafioUtil _desafioUtil = new DesafioUtil();
        private EventoUtil _eventoUtil = new EventoUtil();

        public JogosController(TorneioContext contexto)
        {
            _contexto = contexto;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(_desafioUtil.ConsultarEventos(_contexto));
        }

        public IActionResult Edit(int id){

            var list = _desafioUtil.ConsultaDesafiosPorId(id, _contexto);
                               
            ViewBag.Evento = list.Select(x => x.Nome).First().ToString();
            ViewBag.Fase = list.Select(x => x.Fase).First().ToString();
        
            return View(list);
            
        }

        [HttpPost]
        public IActionResult Edit(List<DesafioView> evento)
        {
            var texte = evento;


            return View();
        }
    }
}
