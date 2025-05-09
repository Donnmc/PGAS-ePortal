﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PGAS.WebAPI.Context;
using PGAS.WebAPI.DTO.portal_v2.View;

namespace PGAS.WebAPI.Controllers.portal_v2.Views
{
    [Route("api/[controller]")]
    [ApiController]
    public class vw_clustered_information_systemController : ControllerBase
    {
        private readonly pgas_eportal_v2Context _context;

        public vw_clustered_information_systemController(pgas_eportal_v2Context context)
        {
            _context = context;
        }

        // GET: api/vw_clustered_information_system
        [HttpGet]
        public async Task<ActionResult<IEnumerable<vw_clustered_information_systemDTO>>> GetGroupedClusters()
        {
            var clusters = await _context.vw_clustered_information_system.ToListAsync();

            var groupedClusters = clusters
                .Where(c => c.Cluster___Is_Active_ == true) // Filter out inactive clusters
                .GroupBy(c => new { c.Cluster_Name, c.Cluster_Abbreviation, c.Cluster_Icon, c.Cluster___Is_Active_ })
                .Select(group => new vw_clustered_information_systemDTO
                {
                    Cluster_Name = group.Key.Cluster_Name,
                    Cluster_Abbreviation = group.Key.Cluster_Abbreviation,
                    Cluster_Icon = group.Key.Cluster_Icon,
                    Cluster___Is_Active_ = group.Key.Cluster___Is_Active_,
                    Information_Systems = group
                        .Where(system => system.Information_System___Is_Active_ == true) // Filter out inactive information systems
                        .Select(system => new InformationSystemDTO
                        {
                            Information_System_Name = system.Information_System_Name,
                            Information_System_Abbreviation = system.Information_System_Abbreviation,
                            Information_System_Icon = system.Information_System_Icon,
                            Information_System_Link = system.Information_System_Link,
                            Information_System___Is_Active_ = system.Information_System___Is_Active_,
                            Information_System_Platform = system.Information_System_Platform,
                        })
                        .ToList()
                })
                .Where(cluster => cluster.Information_Systems.Any()) // Filter clusters with active information systems
                .ToList();

            return groupedClusters;
        }

    }
}