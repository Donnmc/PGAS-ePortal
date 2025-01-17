using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PGAS.WebAPI.Context;
using PGAS.WebAPI.DTO.portal_v2.View;
using PGAS.WebAPI.Entities.portal_v2.Table;

namespace PGAS.WebAPI.Controllers.portal_v2.Tables
{
    [Route("api/[controller]")]
    [ApiController]
    public class external_linkController : ControllerBase
    {
        private readonly pgas_eportal_v2Context _context;
        private readonly TimeZoneInfo PhilippineTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Asia/Manila");

        public external_linkController(pgas_eportal_v2Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<v_external_linkParentListDTO>>> GetNavigationMenuList()
        {
            var menu_list = await _context.vw_external_link.ToListAsync();

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
        public async Task<ActionResult<external_link_parent>> PostParentLink(
        [FromForm] string name,
        [FromForm] string icon,
        [FromForm] string link,
        [FromForm] int order)
        {
            var parentLink = new external_link_parent
            {
                name = name,
                icon = icon,
                link = link,
                order = order,
                date_created = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, PhilippineTimeZone)
            };

            _context.external_link_parent.Add(parentLink);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ParentLinkExists(parentLink.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction(nameof(PostParentLink), new { parentLink.id }, parentLink);
        }

        private bool ParentLinkExists(int id)
        {
            return _context.external_link_parent.Any(e => e.id == id);
        }
    }
}
