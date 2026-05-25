using ControleDeEstoque.Data;
using ControleDeEstoque.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

public class ProdutosController : Controller
{
    private readonly AppDbContext _context;

    public ProdutosController(AppDbContext context)
    {
        _context = context;
    }

    // GET: PRODUTOS
    public async Task<IActionResult> Index()
    {
        return View(await _context.Produtos.Include(p => p.Categoria).ToListAsync());
    }

    // GET: PRODUTOS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var produto = await _context.Produtos
            .FirstOrDefaultAsync(m => m.Id == id);
        if (produto == null) return NotFound();

        return View(produto);
    }

    // GET: PRODUTOS/Create
    public IActionResult Create()
    {
        ViewBag.CategoriaId = new SelectList(_context.Categorias, "Id", "Nome");
        return View();
    }

    // POST: PRODUTOS/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nome,Descricao,Preco,QuantidadeEstoque,CategoriaId")] Produto produto)
    {
        if (ModelState.IsValid)
        {
            _context.Add(produto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.CategoriaId = new SelectList(_context.Categorias, "Id", "Nome", produto.CategoriaId);
        return View(produto);
    }

    // GET: PRODUTOS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var produto = await _context.Produtos.FindAsync(id);
        if (produto == null) return NotFound();

        ViewBag.CategoriaId = new SelectList(_context.Categorias, "Id", "Nome", produto.CategoriaId);
        return View(produto);
    }

    // POST: PRODUTOS/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Nome,Descricao,Preco,QuantidadeEstoque,CategoriaId")] Produto produto)
    {
        if (id != produto.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(produto);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoExists(produto.Id)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        ViewBag.CategoriaId = new SelectList(_context.Categorias, "Id", "Nome", produto.CategoriaId);
        return View(produto);
    }

    // GET: PRODUTOS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var produto = await _context.Produtos
            .FirstOrDefaultAsync(m => m.Id == id);
        if (produto == null) return NotFound();

        return View(produto);
    }

    // POST: PRODUTOS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var produto = await _context.Produtos.FindAsync(id);
        if (produto != null)
        {
            _context.Produtos.Remove(produto);
        }
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ProdutoExists(int? id)
    {
        return _context.Produtos.Any(e => e.Id == id);
    }
}