using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
        private readonly TorneioContext _context;
        private DesafioInfra _desafioInfra = new DesafioInfra();
        private EquipeInfra _equipeInfra = new EquipeInfra();
        private EventoInfra _eventoInfra = new EventoInfra();
        private DesafioUtil _desafioUtil = new DesafioUtil();
        private EventoUtil _eventoUtil = new EventoUtil();
        private JogosUtil _jogosUtil = new JogosUtil();

        public JogosController(TorneioContext contexto)
        {
            _context = contexto;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(_desafioUtil.GetEventos(_context));
        }

        public IActionResult Edit(int id){
            return View(GetListGames(id));
        }

        [HttpPost]
        public IActionResult Edit(int id, JogosView jogos)
        {
            return View(GetListGames(id));
        }

        private JogosView GetListGames(int id)
        {
            var jogos = _jogosUtil.GetByEvento(id, _context);

            if (jogos != null)
            {
                ViewBag.Evento = jogos.Desafios.Select(x => x.Nome).First().ToString();
                ViewBag.Fase = jogos.Desafios.Select(x => x.Fase).First().ToString();
            }

            return jogos;
        }
    }
}
