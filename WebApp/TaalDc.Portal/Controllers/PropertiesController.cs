﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Drawing.Printing;
using System.Globalization;
using AutoMapper;
using NuGet.DependencyResolver;
using TaalDc.Portal.DTO.Catalog;
using TaalDc.Portal.Enums;
using TaalDc.Portal.Services;
using TaalDc.Portal.ViewModels.Catalog;

namespace TaalDc.Portal.Controllers
{
	[Authorize]
	public class PropertiesController : BaseController<PropertiesController>
    {
        private readonly ICatalogService _catalogService;
        private readonly IMapper _mapper;

        public PropertiesController(ICatalogService catalogService, IMapper mapper, ILogger<PropertiesController> loggerInstance) : base(loggerInstance)
        {
            _catalogService = catalogService;
            _mapper = mapper;
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


            return Ok();
        }

        public async Task<IActionResult> EditProperty(int id)
        {
            try
            {
                var result = await _catalogService.GetPropertyById(id);

                if (result == null) RedirectToAction("Index");

                PropertyCreateDTO propertyCreateDto = _mapper.Map<PropertyCreateDTO>(result);

                return View(propertyCreateDto);

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

        public async Task<IActionResult> EditTower(int id)
        {
            try
            {
                var result = await _catalogService.GetTowerById(id);

                if (result == null) RedirectToAction("Index");

                var towerCreateDTO = _mapper.Map<TowerCreateDTO>(result);
                //new TowerCreateDTO(result.Id, result.PropertyId, result.TowerName, result.Address);

                return View(towerCreateDTO);

            }
            catch (Exception err)
            {
                return RedirectToAction("Index");
            }
        }
        
        public async Task<IActionResult> CreateFloor()
        {
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

        public async Task<IActionResult> EditFloor(int id)
        {
            try
            {
                var result = await _catalogService.GetFloorById(id);

                if (result == null) RedirectToAction("Floors");

                var floorCreateDTO = _mapper.Map<FloorCreateDTO>(
                        result);
          
                return View(floorCreateDTO);

            }
            catch (Exception ex)
            {
                return RedirectToAction("Floors");
            }
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

        public async Task<IActionResult> EditUnit(int id)
        {
            var result = await _catalogService.GetUnitById(id);

            if (result == null) RedirectToAction("Units");

            var unitUpdateDto = _mapper.Map<UnitUpdateDTO>(result);
            
            var floors = await _catalogService.GetFloors(null, null, 0, 1, 1000);

            ViewData["Floors"] = floors.Data;

            return View(unitUpdateDto);
        }
        
        [HttpPost("{id}")]
        public async Task<IActionResult> EditUnit(int id, UnitUpdateDTO model)
        {
            if (id != model.UnitId) return BadRequest("Invalid id.");
            
            var result = await _catalogService.UpdateUnit(model);

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


		public async Task<IActionResult> GetUnits(
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
			var units = await _catalogService.GetUnits(filter, floorId, unitTypeId, viewId, statusId, sortBy, sortOrder, pageNumber, pageSize);
            return new JsonResult(units);
		}


		public async Task<IActionResult> UnitTypes()
        {
            var unitTypes = await _catalogService.GetUnitTypes();

            return View(unitTypes);
        }


        public IActionResult CreateUnitType()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUnitType(UnitTypeCreateDTO model)
        {
            var result = await _catalogService.CreateUnitType(model);

            if (!result.IsSuccess) return BadRequest(new
            {
                Message = result.ErrorMessage
            });

            return Ok(result);
        }
    }
}
