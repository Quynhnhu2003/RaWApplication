﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaWMVC.Data;
using RaWMVC.Data.Entities;

namespace RaWMVC.ViewComponents
{
    public class TagList : ViewComponent
    {
        private readonly RaWDbContext _context;
        public TagList(RaWDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var item = await GetItemsAsync();

            return View(item);
        }

        private Task<List<Tag>> GetItemsAsync()
        {
            return _context.Tags
                .OrderBy(t => t.Position)
                .ToListAsync();
        }
    }
}
