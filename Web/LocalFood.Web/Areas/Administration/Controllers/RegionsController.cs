namespace LocalFood.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using LocalFood.Data;
    using LocalFood.Data.Common.Repositories;
    using LocalFood.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    public class RegionsController : AdministrationController
    {
        private readonly IDeletableEntityRepository<Region> regionsRepository;
        private readonly IDeletableEntityRepository<Country> countriesRepository;

        public RegionsController(
            IDeletableEntityRepository<Region> regionsRepository,
            IDeletableEntityRepository<Country> countriesRepository)
        {
            this.regionsRepository = regionsRepository;
            this.countriesRepository = countriesRepository;
        }

        // GET: Administration/Regions
        public async Task<IActionResult> Index()
        {
            var reopsitory = this.regionsRepository.AllWithDeleted().Include(r => r.Country);
            return this.View(await reopsitory.ToListAsync());
        }

        // GET: Administration/Regions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var region = await this.regionsRepository.All()
                .Include(r => r.Country)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (region == null)
            {
                return this.NotFound();
            }

            return View(region);
        }

        // GET: Administration/Regions/Create
        public IActionResult Create()
        {
            this.ViewData["CountryId"] = new SelectList(this.countriesRepository.All(), "Id", "Name");
            return this.View();
        }

        // POST: Administration/Regions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,CountryId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Region region)
        {
            if (this.ModelState.IsValid)
            {
                await this.regionsRepository.AddAsync(region);
                await this.regionsRepository.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }
            this.ViewData["CountryId"] = new SelectList(this.countriesRepository.All(), "Id", "Name", region.CountryId);
            return this.View(region);
        }

        // GET: Administration/Regions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var region = this.regionsRepository.All().FirstOrDefault(x => x.Id == id);
            if (region == null)
            {
                return this.NotFound();
            }

            this.ViewData["CountryId"] = new SelectList(this.countriesRepository.All(), "Id", "Name", region.CountryId);
            return this.View(region);
        }

        // POST: Administration/Regions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,CountryId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Region region)
        {
            if (id != region.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.regionsRepository.Update(region);
                    await this.regionsRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.RegionExists(region.Id))
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

            this.ViewData["CountryId"] = new SelectList(this.countriesRepository.All(), "Id", "Name", region.CountryId);
            return this.View(region);
        }

        // GET: Administration/Regions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var region = await this.regionsRepository.All()
                .Include(r => r.Country)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (region == null)
            {
                return this.NotFound();
            }

            return this.View(region);
        }

        // POST: Administration/Regions/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var region = this.regionsRepository.All().FirstOrDefault(x => x.Id == id);
            this.regionsRepository.Delete(region);
            await this.regionsRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool RegionExists(int id)
        {
            return this.regionsRepository.All().Any(e => e.Id == id);
        }
    }
}
