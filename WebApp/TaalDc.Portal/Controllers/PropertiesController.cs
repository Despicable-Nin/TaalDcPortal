using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Drawing.Printing;
using System.Globalization;
using TaalDc.Portal.Enums;
using TaalDc.Portal.Services;
using TaalDc.Portal.ViewModels.Catalog;

namespace TaalDc.Portal.Controllers
{
    public class PropertiesController : BaseController<PropertiesController>
    {
        private readonly ICatalogService _catalogService;

        public PropertiesController(ICatalogService catalogService, ILogger<PropertiesController> loggerInstance) : base(loggerInstance)
        {
            _catalogService = catalogService;
        }
        public async Task<IActionResult> Index(string filter,
            string sortBy,
            SortOrderEnum sortOrder,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var properties = await _catalogService.GetProperties(filter, sortBy, sortOrder, pageNumber, pageSize);

            return View(properties);
        }


        public async Task<IActionResult> CreateProperty()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProperty(PropertyCreateDTO model)
        {
            var result = await _catalogService.CreateProperty(model);

            if (!result.IsSuccess) return BadRequest(new
            {
                Message = result.ErrorMessage
            });

            return Ok(result);
        }

        public async Task<IActionResult> EditProperty(int id)
        {
            try
            {
                var result = await _catalogService.GetPropertyById(id);

                if (result == null) RedirectToAction("Index");

                var propertyCreateDTO = new PropertyCreateDTO(result.ProjectId, result.Id, result.PropertyName, result.LandArea);

                return View(propertyCreateDTO);

            }catch(Exception err)
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditProperty(PropertyCreateDTO model)
        {
            var result = await _catalogService.CreateProperty(model);

            if (!result.IsSuccess) return BadRequest(new
            {
                Message = result.ErrorMessage
            });

            return Ok(result);
        }

        public async Task<IActionResult> Towers(string filter,
            string sortBy,
            SortOrderEnum sortOrder,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var towers = await _catalogService.GetTowers(filter, sortBy, sortOrder, pageNumber, pageSize);

            return View(towers);
        }
        


        public async Task<IActionResult> CreateTower()
        {

            //get dropdown of properties
           
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateTower(TowerCreateDTO model)
        {
            var result = await _catalogService.CreateTower(model);

            if (!result.IsSuccess) return BadRequest(new
            {
                Message = result.ErrorMessage
            });

            return Ok(result);
        }



        public async Task<IActionResult> CreateFloor()
        {

            //get dropdown of properties

            //get dropdown of towers

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateFloor(FloorCreateDTO model)
        {
            var result = await _catalogService.CreateFloor(model);

            if (!result.IsSuccess) return BadRequest(new
            {
                Message = result.ErrorMessage
            });

            return Ok(result);
        }

        public async Task<IActionResult> Floors(string filter,
            string sortBy,
            SortOrderEnum sortOrder,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var floors = await _catalogService.GetFloors(filter, sortBy, sortOrder, pageNumber, pageSize);

            return View(floors);
        }



        public async Task<IActionResult> CreateUnit()
        {
            //get dropdown of unit types

            //get dropdown of floors
            var floors = await _catalogService.GetFloors(null, null, 0, 1, 1000);

            ViewData["Floors"] = floors.Data;

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateUnit(UnitCreateDTO model)
        {
            var result = await _catalogService.CreateUnit(model);

            if (!result.IsSuccess) return BadRequest(new
            {
                Message = result.ErrorMessage
            });

            return Ok(result);
        }

        public async Task<IActionResult> Units(
            string filter,
            int? floorId,
            int? unitTypeId,
            int? viewId,
            int? statusId,
            string sortBy,
            SortOrderEnum sortOrder,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var units = await _catalogService.GetUnits(filter,floorId,unitTypeId,viewId,statusId, sortBy, sortOrder, pageNumber, pageSize);
            return View(units);
        }
        public async Task<IActionResult> UnitTypes()
        {
            var unitTypes = await _catalogService.GetUnitTypes();

            return View(unitTypes);
        }
    }
}
