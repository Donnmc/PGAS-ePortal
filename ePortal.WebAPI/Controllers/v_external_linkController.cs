using ePortal.WebAPI.Context;
using ePortal.WebAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ePortal.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class v_external_linkController : ControllerBase
    {
        private readonly pgas_eportal_v2Context _context;

        public v_external_linkController(pgas_eportal_v2Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<v_external_linkParentListDTO>>> GetNavigationMenuList()
        {
            var menu_list = await _context.v_external_link.ToListAsync();

            var mappedNavigationMenuList = menu_list
                .GroupBy(p => new { p.Parent_List_Name, p.Parent_List_Icon, p.Parent_List_Link, p.Parent_List_Date_Created, p.Parent_List_Order})
                .OrderBy(p => p.Key.Parent_List_Order)
                .Select(parent => new v_external_linkParentListDTO
                {
                    //parent
                    Parent_List_Name = parent.Key.Parent_List_Name,
                    Parent_List_Icon = parent.Key.Parent_List_Icon,
                    Parent_List_Link = parent.Key.Parent_List_Link,
                    Parent_List_Date_Created = parent.Key.Parent_List_Date_Created,
                    Parent_List_Order = parent.Key.Parent_List_Order,
                    v_external_linkChildList = parent
                    .OrderBy(c => c.Child_List_Order)
                    .Select(child => new v_external_linkChildListDTO
                    {
                        //child
                        Child_List_Name = child.Child_List_Name,
                        Child_List_Icon = child.Child_List_Icon,
                        Child_List_Link = child.Child_List_Link,
                        Child_List_Date_Created = child.Child_List_Date_Created,
                        Child_List_Order = child.Child_List_Order
                    }).ToList()
                }).ToList();

            return mappedNavigationMenuList;
        }
    }
}
