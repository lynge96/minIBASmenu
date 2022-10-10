using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Azure.Data.Tables;
using Azure;
using IBAS_menu.Model;

namespace IBAS_menu.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class MenuController : ControllerBase
    {
        TableClient client;

        private readonly ILogger<MenuController> _logger;

        public MenuController(ILogger<MenuController> logger)
        {
            _logger = logger;

            client = new TableClient(
                new Uri("https://ibasstorageacc.table.core.windows.net/IBASMenu"),
                "IBASMenu",
                new TableSharedKeyCredential("ibasstorageacc", "PIMKpwenxJ5VKfpMwJaR3DAYp4UeTvCMXerG7TOcoOTAjYuJx5ocTVBXW5qpNOGMJXZ48d9JJpgI+AStilpN7w=="));
        }
        
        [HttpGet("getMenu")]
        public IEnumerable<MenuDTO> GetMenu()
        {
            List<MenuDTO> nyListe = new List<MenuDTO>();

            Pageable<TableEntity> entities = client.Query<TableEntity>();


            foreach (TableEntity items in entities)
            {
                MenuDTO ugeMenu = new MenuDTO();

                ugeMenu.Dag = items.RowKey;
                ugeMenu.VarmRet = items.GetString("Varmret");
                ugeMenu.KoldRet = items.GetString("Koldret");
            }

            return nyListe;
        }
    }
}
