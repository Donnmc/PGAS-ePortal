using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PGAS.WebAPI.Context;
using PGAS.WebAPI.DTO.portal_v2;
using PGAS.WebAPI.Entities.portal_v2.Table;

namespace PGAS.WebAPI.Controllers.ePortal.Tables
{
    [Route("api/[controller]")]
    [ApiController]
    public class external_linkController : ControllerBase
    {
        private readonly pgas_eportal_v2Context _context;

        public external_linkController(pgas_eportal_v2Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<v_external_linkParentListDTO>>> GetNavigationMenuList()
        {
            var menu_list = await _context.v_external_link.ToListAsync();

            var mappedNavigationMenuList = menu_list
                .GroupBy(p => new { p.Parent_List_Name, p.Parent_List_Icon, p.Parent_List_Link, p.Parent_List_Date_Created, p.Parent_List_Order })
                .OrderBy(p => p.Key.Parent_List_Order)
                .Select(parent => new v_external_linkParentListDTO
                {
                    Parent_List_Name = parent.Key.Parent_List_Name,
                    Parent_List_Icon = parent.Key.Parent_List_Icon,
                    Parent_List_Link = parent.Key.Parent_List_Link,
                    Parent_List_Date_Created = parent.Key.Parent_List_Date_Created,
                    Parent_List_Order = parent.Key.Parent_List_Order,
                    v_external_linkChildList = parent
                    .OrderBy(c => c.Child_List_Order)
                    .Select(child => new v_external_linkChildListDTO
                    {
                        Child_List_Name = child.Child_List_Name,
                        Child_List_Icon = child.Child_List_Icon,
                        Child_List_Link = child.Child_List_Link,
                        Child_List_Date_Created = child.Child_List_Date_Created,
                        Child_List_Order = child.Child_List_Order
                    }).ToList()
                }).ToList();

            return mappedNavigationMenuList;
        }



        [HttpPost("parent")]
        public async Task<IActionResult> CreateExternalLinkParent([FromForm] external_link_parent externalLinkParent)
        {
            if (externalLinkParent == null)
            {
                return BadRequest("Invalid data.");
            }

            externalLinkParent.date_created = DateTime.UtcNow;

            _context.Set<external_link_parent>().Add(externalLinkParent);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetExternalLinkParent), new { id = externalLinkParent.id }, externalLinkParent);
        }

        [HttpGet("parent/{id}")]
        public async Task<IActionResult> GetExternalLinkParent(int id)
        {
            var externalLinkParent = await _context.Set<external_link_parent>()
                                                   .Include(e => e.external_link_child)
                                                   .FirstOrDefaultAsync(e => e.id == id);

            if (externalLinkParent == null)
            {
                return NotFound();
            }

            return Ok(externalLinkParent);
        }

        [HttpPost("child")]
        public async Task<IActionResult> CreateExternalLinkChild([FromForm] external_link_child externalLinkChild)
        {
            if (externalLinkChild == null)
            {
                return BadRequest("Invalid data.");
            }

            externalLinkChild.date_created = DateTime.UtcNow;

            _context.Set<external_link_child>().Add(externalLinkChild);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetExternalLinkChild), new { id = externalLinkChild.id }, externalLinkChild);
        }

        [HttpGet("child/{id}")]
        public async Task<IActionResult> GetExternalLinkChild(int id)
        {
            var externalLinkChild = await _context.Set<external_link_child>()
                                                  .Include(e => e.parent)
                                                  .FirstOrDefaultAsync(e => e.id == id);

            if (externalLinkChild == null)
            {
                return NotFound();
            }

            return Ok(externalLinkChild);
        }
    }
}
