using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LoteriaWeb.Data;
using LoteriaWeb.Models;

namespace LoteriaWeb.Controllers
{
    public class ApostasController : Controller
    {
        private readonly LoteriaDbContext _context;

        public ApostasController(LoteriaDbContext context)
        {
            _context = context;
        }

        // GET: Apostas
        public async Task<IActionResult> Index()
        {
            var apostas = await _context.Apostas.ToListAsync();
            return View(apostas);
        }

        // GET: Apostas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var aposta = await _context.Apostas.FirstOrDefaultAsync(a => a.Id == id);
            if (aposta == null) return NotFound();

            return View(aposta);
        }

        // GET: Apostas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Apostas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomeJogador,NumerosSelecionados")] Aposta aposta)
        {
            if (ModelState.IsValid)
            {
                aposta.DataAposta = DateTime.Now;
                _context.Add(aposta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aposta);
        }

        // GET: Apostas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var aposta = await _context.Apostas.FindAsync(id);
            if (aposta == null) return NotFound();

            return View(aposta);
        }

        // POST: Apostas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomeJogador,NumerosSelecionados,DataAposta")] Aposta aposta)
        {
            if (id != aposta.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aposta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApostaExists(aposta.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(aposta);
        }

        // GET: Apostas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var aposta = await _context.Apostas.FirstOrDefaultAsync(a => a.Id == id);
            if (aposta == null) return NotFound();

            return View(aposta);
        }

        // POST: Apostas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aposta = await _context.Apostas.FindAsync(id);
            if (aposta != null)
            {
                _context.Apostas.Remove(aposta);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ApostaExists(int id)
        {
            return _context.Apostas.Any(e => e.Id == id);
        }

        // Rota alternativa caso venha a ser usada depois (formulário personalizado)
        public IActionResult Apostar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Apostar(string nomeJogador, string numerosSelecionados)
        {
            if (string.IsNullOrWhiteSpace(nomeJogador) || string.IsNullOrWhiteSpace(numerosSelecionados))
            {
                ModelState.AddModelError("", "Nome e números são obrigatórios.");
                return View();
            }

            var novaAposta = new Aposta
            {
                NomeJogador = nomeJogador,
                NumerosSelecionados = numerosSelecionados,
                DataAposta = DateTime.Now
            };

            _context.Apostas.Add(novaAposta);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
