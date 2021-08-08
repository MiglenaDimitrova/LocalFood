namespace LocalFood.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using LocalFood.Data.Common.Repositories;
    using LocalFood.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class MarketsController : Controller
    {
        private readonly IDeletableEntityRepository<Market> marketsRepository;

        public MarketsController(IDeletableEntityRepository<Market> marketsRepository)
        {
            this.marketsRepository = marketsRepository;
        }

        // GET: Administration/Markets
        public async Task<IActionResult> Index()
        {
            return this.View(await this.marketsRepository.All().ToListAsync());
        }

        // GET: Administration/Markets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var market = await this.marketsRepository.All()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (market == null)
            {
                return this.NotFound();
            }

            return this.View(market);
        }

        // GET: Administration/Markets/Create
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Administration/Markets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,UrlLocation,FacebookPage,FullAddress,Description,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Market market)
        {
            if (this.ModelState.IsValid)
            {
                await this.marketsRepository.AddAsync(market);
                await this.marketsRepository.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }
            return this.View(market);
        }

        // GET: Administration/Markets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var market = this.marketsRepository.All().FirstOrDefault(x => x.Id == id);
            if (market == null)
            {
                return this.NotFound();
            }

            return this.View(market);
        }

        // POST: Administration/Markets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,UrlLocation,FacebookPage,FullAddress,Description,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Market market)
        {
            if (id != market.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.marketsRepository.Update(market);
                    await this.marketsRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.MarketExists(market.Id))
                    {
                        return this.NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(market);
        }

        // GET: Administration/Markets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var market = await this.marketsRepository.All()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (market == null)
            {
                return this.NotFound();
            }

            return this.View(market);
        }

        // POST: Administration/Markets/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var market = this.marketsRepository.All().FirstOrDefault(x => x.Id == id);
            this.marketsRepository.Delete(market);
            await this.marketsRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool MarketExists(int id)
        {
            return this.marketsRepository.All().Any(e => e.Id == id);
        }
    }
}
