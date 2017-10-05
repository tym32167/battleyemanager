using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BattlEyeManagerWeb.Pages
{
    public class SamplePageModel : PageModel
    {
        private static readonly ConcurrentBag<SampleViewModel> Repo = new ConcurrentBag<SampleViewModel>();

        public string Title => "Hello from model!";

        public List<SampleViewModel> Items { get; set; }

        [BindProperty]
        public SampleViewModel ItemToAdd { get; set; } 

        public IActionResult OnGet()
        {
            Items = new List<SampleViewModel>(Repo);
            return Page();
        }

        public IActionResult OnPostAdd()
        {
            if (!ModelState.IsValid) return OnGet();
            if (Repo.Count > 5)
            {
                return OnGet();
            }
            var newItem = new SampleViewModel { Id = Repo.Count, Name = ItemToAdd.Name };
            Repo.Add(newItem);
            return RedirectToPage();
        }
    }

    public class SampleViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}