using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Data;
using WebApplication2.Models;
using WebApplication2.ViewModels;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext context;

        public HomeController(ApplicationDbContext dbContext)
        {
            this.context = dbContext;
        }

        static public int editID;
        static public string editkeyelement;
        static public string editvalueelement;
        static public string Searchstr;

        public IActionResult Index()
        {

            IndexViewModel indexViewModel = new IndexViewModel();

            return View(indexViewModel);
        }

        public IActionResult Error()
        {

            return View();
        }


        [HttpGet]
        public IActionResult Result()
        {
            List<Dictelement> TheDictionary = context.Dictelements.ToList();

            if (TheDictionary.Count > 0)
            {
                ResultViewModel resultViewModel = new ResultViewModel();

                resultViewModel.TheDictionary = TheDictionary;

                return View(resultViewModel);
            }

            else
            {
                return Redirect("/");
            }
        }

        [HttpPost]
        public IActionResult Result(ResultViewModel resultViewModel)

        {
            List<Dictelement> TheDictionary = context.Dictelements.ToList();

            if (ModelState.IsValid)
            {
                Dictelement addelem = new Dictelement(resultViewModel.NewElement1.ToLower(), resultViewModel.NewElement2.ToLower());
                TheDictionary.Add(addelem);
                context.Dictelements.Add(addelem);
                context.SaveChanges();

                resultViewModel.TheDictionary = TheDictionary;

                return View(resultViewModel);
            }

            return Redirect("/Home/Error");

        }

        [HttpGet]
        public IActionResult Remove()
        {
            List<Dictelement> TheDictionary = context.Dictelements.ToList();

            if (TheDictionary.Count > 0)
            {
                RemoveViewModel removeViewModel = new RemoveViewModel();

                removeViewModel.TheDictionary = TheDictionary;

                return View(removeViewModel);
            }

            else
            {
                return Redirect("/");
            }
        }

        [HttpPost]
        public IActionResult Remove(RemoveViewModel removeViewModel)

        {
            List<Dictelement> TheDictionary = context.Dictelements.ToList();

            if (ModelState.IsValid)
            {

                Dictelement remelem = context.Dictelements.Single(c => c.ID == removeViewModel.NewElement1);
                TheDictionary.RemoveAll(x => x.ID == removeViewModel.NewElement1);
                context.Dictelements.Remove(remelem);
                context.SaveChanges();

                removeViewModel.TheDictionary = TheDictionary;

                return Redirect("/Home/Result");
            }

            return Redirect("/Home/Error");

        }

        [HttpGet]
        public IActionResult EditSelect()
        {
            List<Dictelement> TheDictionary = context.Dictelements.ToList();

            if (TheDictionary.Count > 0)
            {
                EditSelectViewModel editSelectViewModel = new EditSelectViewModel();

                editSelectViewModel.TheDictionary = TheDictionary;

                return View(editSelectViewModel);
            }

            else
            {
                return Redirect("/");
            }
        }


        [HttpPost]
        public IActionResult EditSelect(EditSelectViewModel editSelectViewModel)
        {
            List<Dictelement> TheDictionary = context.Dictelements.ToList();

            if (ModelState.IsValid)
            {

                Dictelement editelem = context.Dictelements.Single(c => c.ID == editSelectViewModel.NewElement1);
                editID = editSelectViewModel.NewElement1;
                editkeyelement = editelem.Keyelement;
                editvalueelement = editelem.Valueelement;
                ViewBag.editID = editID;
                ViewBag.valueelement = editvalueelement;

                return View("EditItem");

            }

            return Redirect("/Home/Error");
        }


        [HttpGet]
        public IActionResult EditItem()
        {
            List<Dictelement> TheDictionary = context.Dictelements.ToList();

            if (TheDictionary.Count > 0)
            {

                EditItemViewModel editItemViewModel = new EditItemViewModel();

                return View(editItemViewModel);
            }

            else
            {
                return Redirect("/");
            }
        }

        [HttpPost]
        public IActionResult EditItem(EditItemViewModel editItemViewModel)

        {
            List<Dictelement> TheDictionary = context.Dictelements.ToList();

            if (ModelState.IsValid)

            {
                Dictelement editelem = context.Dictelements.Single(c => c.ID == editID);
                editelem.Valueelement = editItemViewModel.NewElement2;
                context.SaveChanges();

                return Redirect("/Home/Result");
            }

            return Redirect("/Home/Error");

        }

        [HttpGet]
        public IActionResult SearchSelect()
        {
            List<Dictelement> TheDictionary = context.Dictelements.ToList();

            if (TheDictionary.Count > 0)
            {
                SearchSelectViewModel searchSelectViewModel = new SearchSelectViewModel();

                return View(searchSelectViewModel);
            }

            else
            {
                return Redirect("/");
            }
        }

        [HttpPost]
        public IActionResult SearchSelect(SearchSelectViewModel searchSelectViewModel)

        {
            List<Dictelement> TheDictionary = context.Dictelements.ToList();

            if (ModelState.IsValid)

            {
                Searchstr = searchSelectViewModel.Searchstr;
                return Redirect("/Home/SearchResult");
            }

            return Redirect("/Home/Error");

        }

        [HttpGet]
        public IActionResult SearchResult()
        {
            List<Dictelement> TheDictionary = context.Dictelements.ToList();

            if (TheDictionary.Count > 0)
            {
                SearchResultViewModel searchResultViewModel = new SearchResultViewModel();

                List<string> Anslist = new List<string>();

                foreach (var item in TheDictionary)

                {
                    if (item.Valueelement.Contains(Searchstr))

                    {

                        string addstr = item.Keyelement + ":" + item.Valueelement;
                        Anslist.Add(addstr);

                    }

                }

                if (Anslist.Count == 0)
                {

                    Anslist.Add("That search returned no results.");

                }


                ViewBag.Anslist = Anslist;

                return View(searchResultViewModel);
            }

            else
            {
                return Redirect("/");
            }
        }

        [HttpGet]
        public IActionResult Sort()
        {
            List<Dictelement> TheDictionary = context.Dictelements.ToList();

            if (TheDictionary.Count > 0)
            {
                SortViewModel sortViewModel = new SortViewModel();

                List<Dictelement> SortedList = TheDictionary.OrderBy(o => o.Keyelement).ToList();

                sortViewModel.SortedDict = SortedList;

                return View(sortViewModel);
            }

            else
            {
                return Redirect("/");
            }
        }

    }

}