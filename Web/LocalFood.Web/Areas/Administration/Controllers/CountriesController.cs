namespace LocalFood.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using LocalFood.Data.Common.Repositories;
    using LocalFood.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class CountriesController : AdministrationController
    {
        private readonly IDeletableEntityRepository<Country> categoriesRepository;

        public CountriesController(IDeletableEntityRepository<Country> categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        // GET: Administration/Countries
        public async Task<IActionResult> Index()
        {
            return this.View(await this.categoriesRepository.AllWithDeleted().ToListAsync());
        }

        // GET: Administration/Countries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var country = await this.categoriesRepository.All()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (country == null)
            {
                return this.NotFound();
            }

            return this.View(country);
        }

        // GET: Administration/Countries/Create
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Administration/Countries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Country country)
        {
            if (this.ModelState.IsValid)
            {
                await this.categoriesRepository.AddAsync(country);
                await this.categoriesRepository.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(country);
        }

        // GET: Administration/Countries/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var country = this.categoriesRepository.All().FirstOrDefault(x => x.Id == id);
            if (country == null)
            {
                return this.NotFound();
            }

            return this.View(country);
        }

        // POST: Administration/Countries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Country country)
        {
            if (id != country.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.categoriesRepository.Update(country);
                    await this.categoriesRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.CountryExists(country.Id))
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

            return this.View(country);
        }

        // GET: Administration/Countries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var country = await this.categoriesRepository.All()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (country == null)
            {
                return this.NotFound();
            }

            return this.View(country);
        }

        // POST: Administration/Countries/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var country = this.categoriesRepository.All().FirstOrDefault(x => x.Id == id);
            this.categoriesRepository.Delete(country);
            await this.categoriesRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool CountryExists(int id)
        {
            return this.categoriesRepository.All().Any(e => e.Id == id);
        }
    }
}
