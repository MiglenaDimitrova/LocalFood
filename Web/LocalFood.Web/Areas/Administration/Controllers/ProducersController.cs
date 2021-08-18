namespace LocalFood.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using LocalFood.Data.Common.Repositories;
    using LocalFood.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    public class ProducersController : AdministrationController
    {
        private readonly IDeletableEntityRepository<Producer> producersRepository;
        private readonly IRepository<Image> imagesRepository;
        private readonly IDeletableEntityRepository<Location> locationsRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;

        public ProducersController(
            IDeletableEntityRepository<Producer> producersRepository,
            IRepository<Image> imagesRepository,
            IDeletableEntityRepository<Location> locationsRepository,
            IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.producersRepository = producersRepository;
            this.imagesRepository = imagesRepository;
            this.locationsRepository = locationsRepository;
            this.usersRepository = usersRepository;
        }

        // GET: Administration/Producers
        public async Task<IActionResult> Index()
        {
            var repository = this.producersRepository.AllWithDeleted().Include(p => p.ApplicationUser).Include(p => p.Image).Include(p => p.Location);
            return this.View(await repository.ToListAsync());
        }

        // GET: Administration/Producers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var producer = await this.producersRepository.All()
                .Include(p => p.ApplicationUser)
                .Include(p => p.Image)
                .Include(p => p.Location)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producer == null)
            {
                return this.NotFound();
            }

            return this.View(producer);
        }

        // GET: Administration/Producers/Create
        public IActionResult Create()
        {
            this.ViewData["ApplicationUserId"] = new SelectList(this.usersRepository.All(), "Id", "Id");
            this.ViewData["ImageId"] = new SelectList(this.imagesRepository.All(), "Id", "Id");
            this.ViewData["LocationId"] = new SelectList(this.locationsRepository.All(), "Id", "Adress");
            return this.View();
        }

        // POST: Administration/Producers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApplicationUserId,FirstName,LastName,CompanyName,PhoneNumber,Email,Site,Description,ImageId,LocationId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Producer producer)
        {
            if (this.ModelState.IsValid)
            {
                await this.producersRepository.AddAsync(producer);
                await this.producersRepository.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["ApplicationUserId"] = new SelectList(this.usersRepository.All(), "Id", "Id", producer.ApplicationUserId);
            this.ViewData["ImageId"] = new SelectList(this.imagesRepository.All(), "Id", "Id", producer.ImageId);
            this.ViewData["LocationId"] = new SelectList(this.locationsRepository.All(), "Id", "Adress", producer.LocationId);
            return this.View(producer);
        }

        // GET: Administration/Producers/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var producer = this.producersRepository.All().FirstOrDefault(x => x.Id == id);
            if (producer == null)
            {
                return this.NotFound();
            }

            this.ViewData["ApplicationUserId"] = new SelectList(this.usersRepository.All(), "Id", "Id", producer.ApplicationUserId);
            this.ViewData["ImageId"] = new SelectList(this.imagesRepository.All(), "Id", "Id", producer.ImageId);
            this.ViewData["LocationId"] = new SelectList(this.locationsRepository.All(), "Id", "Adress", producer.LocationId);
            return this.View(producer);
        }

        // POST: Administration/Producers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ApplicationUserId,FirstName,LastName,CompanyName,PhoneNumber,Email,Site,Description,ImageId,LocationId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Producer producer)
        {
            if (id != producer.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.producersRepository.Update(producer);
                    await this.producersRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.ProducerExists(producer.Id))
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

            this.ViewData["ApplicationUserId"] = new SelectList(this.usersRepository.All(), "Id", "Id", producer.ApplicationUserId);
            this.ViewData["ImageId"] = new SelectList(this.imagesRepository.All(), "Id", "Id", producer.ImageId);
            this.ViewData["LocationId"] = new SelectList(this.locationsRepository.All(), "Id", "Adress", producer.LocationId);
            return this.View(producer);
        }

        // GET: Administration/Producers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var producer = await this.producersRepository.All()
                .Include(p => p.ApplicationUser)
                .Include(p => p.Image)
                .Include(p => p.Location)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producer == null)
            {
                return this.NotFound();
            }

            return this.View(producer);
        }

        // POST: Administration/Producers/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producer = this.producersRepository.All().FirstOrDefault(x => x.Id == id);
            this.producersRepository.Delete(producer);
            await this.producersRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool ProducerExists(int id)
        {
            return this.producersRepository.All().Any(e => e.Id == id);
        }
    }
}
