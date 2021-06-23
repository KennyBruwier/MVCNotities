using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCNotities.Database;
using MVCNotities.Models;
using StoreAccountingApp.CustomMethods;

namespace MVCNotities.Controllers
{
    public class NotitiesController : Controller
    {
        private readonly NotitiesContext _context;
        private readonly IWebHostEnvironment hostEnvironment;

        public NotitiesController(NotitiesContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this.hostEnvironment = hostEnvironment;
        }

        // GET: Notities
        public async Task<IActionResult> Index()
        {
            return View(await _context.Notities.ToListAsync());
        }

        // GET: Notities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notitie = await _context.Notities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (notitie == null)
            {
                return NotFound();
            }

            return View(notitie);
        }

        // GET: Notities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Notities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Omschrijving,ImgPath,UploadFile,Datum")] NotitieCreateViewModel notitie)
        {
            if (ModelState.IsValid)
            {
                Notitie newNotitie = ObjMethods.CopyProperties<NotitieCreateViewModel, Notitie>(notitie);
                if (notitie.UploadFile != null)
                {
                    string uniqueFileName = UploadFoto(notitie.UploadFile);
                    newNotitie.ImgPath = "/"+ Path.Combine("Fotos",uniqueFileName);

                }
                _context.Add(newNotitie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(notitie);
        }

        // GET: Notities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notitie = await _context.Notities.FindAsync(id);
            if (notitie == null)
            {
                return NotFound();
            }
            return View(ObjMethods.CopyProperties<Notitie,NotitieEditViewModel>( notitie));
        }

        // POST: Notities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, NotitieEditViewModel notitie)
        {
            if (id != notitie.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {
                    var dbNotitie = _context.Find<Notitie>(id);
                    //dbNotitie = ObjMethods.CopyProperties<NotitieEditViewModel, Notitie>(notitie);
                    if (notitie.UploadFile != null)
                    {
                        if (!String.IsNullOrEmpty(dbNotitie.ImgPath))
                        {
                            DeletePhoto(dbNotitie.ImgPath);
                        }
                        string uniqueFileName = UploadFoto(notitie.UploadFile);
                        dbNotitie.ImgPath = Path.Combine("/Fotos", uniqueFileName);
                    }

                    _context.Update(dbNotitie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotitieExists(notitie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(notitie);
        }

        // GET: Notities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notitie = await _context.Notities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (notitie == null)
            {
                return NotFound();
            }

            return View(notitie);
        }

        // POST: Notities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var notitie = await _context.Notities.FindAsync(id);
            _context.Notities.Remove(notitie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotitieExists(int id)
        {
            return _context.Notities.Any(e => e.Id == id);
        }
        private string UploadFoto(IFormFile foto)
        {
            string uniqueFilename = Guid.NewGuid().ToString() + Path.GetExtension(foto.FileName);
            string pathName = Path.Combine(hostEnvironment.WebRootPath, "Fotos");
            string fileNameWithPath = Path.Combine(pathName, uniqueFilename);
            //return fileNameWithPath;
            using (var stream = new FileStream(fileNameWithPath,FileMode.Create))
            {
                foto.CopyTo(stream);
            }
            return uniqueFilename;
        }
        private void DeletePhoto(string photoUrl)
        {
            string path = Path.Combine(hostEnvironment.WebRootPath, photoUrl.Substring(1));
            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);
        }
    }
}
