using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using SeedWork;
using System;
using System.Reflection;
using TaalDc.Portal.DTO.Sales;
using TaalDc.Portal.Services;
using TaalDc.Portal.ViewModels.Catalog;
using TaalDc.Portal.ViewModels.Sales;

namespace TaalDc.Portal.Controllers;

[Authorize]
public class SalesController : BaseController<SalesController>
{
    private const int AVAILABLE = 1;
    private const int SOLD = 2;
    private const int RESERVED = 3;
    private const int BLOCKED = 4;

    public static IDictionary<int, string> Dictionary = new Dictionary<int, string>
    {
        { 1, "ForReservation" },
        { 2, "ForAcquisition" }
    };

    private readonly IAccountService _accountService;
    private readonly ICatalogService _catalogService;
    private readonly IAmCurrentUser _currentUser;
    private readonly ISalesService _salesService;


    public SalesController(ILogger<SalesController> loggerInstance, ISalesService salesService,
        ICatalogService catalogService, IAccountService accountService, IAmCurrentUser currentUser) : base(
        loggerInstance)
    {
        _salesService = salesService;
        _catalogService = catalogService;
        _accountService = accountService;
        _currentUser = currentUser;
    }

    public static int GetTypeId(string name)
    {
        return Dictionary.SingleOrDefault(i => i.Value.ToLower() == name.ToLower()).Key;
    }

    public async Task<IActionResult> Index(int? floorId,
        int? unitTypeId,
        int? viewId,
        string? filter,
        int pageNumber = 1,
        int pageSize = 10)
    {
        //display units by joining tables unitreplica and acquisition
        //this should tell us which units are available,
        //w/c has been Reserved w/o payment, Reserved w/ payment,
        //w/c has paid for Downpayment also it can tell us Cancelled (in history -- for future use case)
        //
        var broker = _currentUser.IsBroker() ? _currentUser.Email : string.Empty;
        var sales = await _salesService.GetUnitAndOrdersAvailability(SOLD, pageNumber, pageSize, floorId, unitTypeId,
            viewId, filter, broker);
        return View(sales);
    }


    public async Task<IActionResult> Available(int? floorId,
        int? unitTypeId,
        int? viewId,
        string? filter,
        int pageNumber = 1,
        int pageSize = 10)
    {
        //display units by joining tables unitreplica and acquisition
        //this should tell us which units are available,
        //w/c has been Reserved w/o payment, Reserved w/ payment,
        //w/c has paid for Downpayment also it can tell us Cancelled (in history -- for future use case)
        //

        var broker = _currentUser.IsBroker() ? _currentUser.Email : string.Empty;
        var sales = await _salesService.GetUnitAndOrdersAvailability(AVAILABLE, pageNumber, pageSize, floorId,
            unitTypeId, viewId, filter, broker);
        return View(sales);
    }


    public async Task<IActionResult> Reserved(
        int? floorId,
        int? unitTypeId,
        int? viewId,
        string? filter,
        int pageNumber = 1,
        int pageSize = 10
    )
    {
        //display units by joining tables unitreplica and acquisition
        //this should tell us which units are available,
        //w/c has been Reserved w/o payment, Reserved w/ payment,
        //w/c has paid for Downpayment also it can tell us Cancelled (in history -- for future use case)
        var broker = _currentUser.IsBroker() ? _currentUser.Email : string.Empty;
        var sales = await _salesService.GetUnitAndOrdersAvailability(RESERVED, pageNumber, pageSize, floorId,
            unitTypeId, viewId,filter, broker);


        return View(sales);
    }

    public async Task<IActionResult> Blocked(int? floorId,
        int? unitTypeId,
        int? viewId,
        string? filter,
        int pageNumber = 1,
        int pageSize = 10)
    {
        //display units by joining tables unitreplica and acquisition
        //this should tell us which units are available,
        //w/c has been Reserved w/o payment, Reserved w/ payment,
        //w/c has paid for Downpayment also it can tell us Cancelled (in history -- for future use case)
        //
        var broker = _currentUser.IsBroker() ? _currentUser.Email : string.Empty;
        var sales = await _salesService.GetUnitAndOrdersAvailability(BLOCKED, pageNumber, pageSize, floorId, unitTypeId,
            viewId, filter, broker);


        return View(sales);
    }

    public IActionResult Cancelled()
    {
        //display units by joining tables unitreplica and acquisition
        //this should tell us which units are available,
        //w/c has been Reserved w/o payment, Reserved w/ payment,
        //w/c has paid for Downpayment also it can tell us Cancelled (in history -- for future use case)
        //


        return View();
    }

    public IActionResult Create()
    {
        var salesCreateDTO = new AddBuyerOrderRequest();
        return View(salesCreateDTO);
    }

    public async Task<IActionResult> Report(DateTime from, DateTime to)
    {
        var result = await _salesService.GetOrdersByDate(from, to);

        byte[] excelBytes = CreateExcelFile(result.ToList());
        return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Sales.xlsx");
    }


    public byte[] CreateExcelFile(List<OrderReportResponse> orders)
    {
        // Create a new Excel package
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using (ExcelPackage excelPackage = new ExcelPackage())
        {
            // Add a new worksheet to the Excel package
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sales");

            // Write the header row
            int headerRow = 1;
            PropertyInfo[] properties = typeof(OrderReportResponse).GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                worksheet.Cells[headerRow, i + 1].Value = properties[i].Name;
            }

            // Write the data rows
            int dataRow = 2;
            foreach (var obj in orders)
            {
                for (int i = 0; i < properties.Length; i++)
                {
                    object value = properties[i].GetValue(obj);

                    if (properties[i].PropertyType == typeof(DateTime) || properties[i].PropertyType == typeof(DateTimeOffset)) { 
                        worksheet.Cells[dataRow, i + 1].Style.Numberformat.Format = "yyyy-mm-dd";
                    }

                    worksheet.Cells[dataRow, i + 1].Value = value;
                }
                dataRow++;
            }


            // Convert the Excel package to a byte array
            byte[] excelBytes = excelPackage.GetAsByteArray();
            return excelBytes;
        }
    }



    [HttpPost]
    public async Task<IActionResult> Create(AddBuyerOrderRequest model)
    {
        await ValidateFees(model);
        await ValidateBroker(model);


        if (ModelState.IsValid)
        {
            var result = await _salesService.AddBuyerOrder(model);

            if (!result.IsSuccess)
                return BadRequest(new
                {
                    IsFormError = false,
                    Message = result.ErrorMessage
                });

            //Update Unit Status in Catalog
            //var unitStatus =
            //    new UnitStatusUpdate_ClientDto(model.UnitId, 3, $"Reserved to {model.FirstName} {model.LastName}");
            //var unitStatusResult = await _catalogService.UpdateUnitStatus(unitStatus);

            //if (!unitStatusResult.IsSuccess)
            //    return BadRequest(new { IsFormError = false, Message = unitStatusResult.ErrorMessage });

            return Ok(result);
        }

        return BadRequest(new
        {
            IsFormError = true,
            Message = "Please check your data entry.",
            ModelState = Json(ModelState)
        });
    }


    [HttpPost]
    public async Task<IActionResult> AcceptPayment(int orderId, int paymentId, int paymentTypeId, string confirmationNumber)
    {
        if (orderId > 0 && paymentId > 0)
        {
            var result = await _salesService.AcceptPayment(orderId, paymentId, confirmationNumber);

            if (!result.IsSuccess)
                return BadRequest(new
                {
                    IsFormError = false,
                    Message = result.ErrorMessage
                });

            //var order = await _salesService.GetSalesById(orderId);

            //update Units On Catalog --> Catalog then Replies to update UnitReplica
            //int unitStatusId = 3;

            //if(paymentTypeId == 3)
            //{
            //    unitStatusId = 2;
            //}
            //else if (paymentTypeId <= 2)
            //{
            //    unitStatusId = 3;
            //}

            //var unitStatus =
            //   new UnitStatusUpdate_ClientDto(order.UnitId.Value, unitStatusId, "");
            
            //await _catalogService.UpdateUnitStatus(unitStatus);

            //if (!unitStatusResult.IsSuccess)
            //    return BadRequest(new { IsFormError = false, Message = unitStatusResult.ErrorMessage });

            return Ok(result);
        }

        return BadRequest(new
        {
            IsFormError = false,
            ErrorMessage = "Invalid order and payment id"
        });
    }


    [HttpPost]
    public async Task<IActionResult> VoidPayment(int orderId, int paymentId)
    {
        if (orderId > 0 && paymentId > 0)
        {
            var result = await _salesService.VoidPayment(orderId, paymentId);

            if (!result.IsSuccess)
                return BadRequest(new
                {
                    IsFormError = false,
                    Message = result.ErrorMessage
                });

            return Ok(result);
        }

        return BadRequest(new
        {
            IsFormError = false,
            ErrorMessage = "Invalid order and payment id"
        });
    }


    [HttpPost]
    public async Task<IActionResult> CreatePayment(int id, AddPaymentRequest model)
    {
        model.TransactionId = id;

        if (model.TransactionId > 0)
        {
            model.TransactionTypeId =
                model.PaymentTypeId != 1 ? GetTypeId("ForAcquisition") : GetTypeId("ForReservation");

            var result = await _salesService.AddPayment(model);

            if (!result.IsSuccess)
                return BadRequest(new
                {
                    IsFormError = false,
                    Message = result.ErrorMessage
                });

            return Ok(result);
        }

        return BadRequest(new
        {
            IsFormError = false,
            ErrorMessage = "Invalid payment id"
        });
    }

    [HttpPost]
    public async Task<IActionResult> EditPayment(EditPaymentRequest model)
    {
        if (model.OrderId > 0)
        {
            model.TransactionTypeId =
                model.PaymentTypeId != 1 ? GetTypeId("ForAcquisition") : GetTypeId("ForReservation");

            var result = await _salesService.UpdatePayment(model);

            if (!result.IsSuccess)
                return BadRequest(new
                {
                    IsFormError = false,
                    Message = result.ErrorMessage
                });

            return Ok(result);
        }

        return BadRequest(new
        {
            IsFormError = false,
            ErrorMessage = "Invalid payment id"
        });
    }


    [Route("Sales/{id}/Details")]
    public async Task<IActionResult> Details(int id)
    {
        var result = await _salesService.GetSalesById(id);

        return View(result);
    }

    [Route("Sales/{id}/Payments")]
    public async Task<IActionResult> Payments(int id)
    {
        var payments = await _salesService.GetSalesPayments(id);
        var salesViewModel = new SalesViewModel(payments.OrderBy(i => i.ActualPaymentDate), null, id);

        return View(salesViewModel);
    }


    [Route("Sales/{id}/Units")]
    public async Task<IActionResult> Units(int id)
    {
        var units = await _salesService.GetContractOrderItems(id);
        var salesViewModel = new SalesViewModel(null, units, id);

        return View(salesViewModel);
    }




    public async Task<IActionResult> ExpiredReservations()
    {
        var expiredReservations = await _salesService.GetExpiredReservations();

        return View(expiredReservations);
    }


    [HttpPost]
    public async Task<IActionResult> ExtendReservation(ExtendReservationRequest model)
    {
        if (ModelState.IsValid)
        {
            var result = await _salesService.ExtendReservation(model);

            if (!result.IsSuccess)
                return BadRequest(new
                {
                    IsFormError = false,
                    Message = result.ErrorMessage
                });

            return Ok(result);
        }

        return BadRequest(new
        {
            IsFormError = true,
            Message = "Please check your data entry.",
            ModelState = Json(ModelState)
        });
    }

    [HttpPost]
    public async Task<IActionResult> CancelReservation(ForfeitReservationRequest model)
    {
        if (ModelState.IsValid)
        {
            var result = await _salesService.ForfeitReservation(model);

            if (!result.IsSuccess)
                return BadRequest(new
                {
                    IsFormError = false,
                    Message = result.ErrorMessage
                });

            return Ok(result);
        }

        return BadRequest(new
        {
            IsFormError = true,
            Message = "Please check your data entry.",
            ModelState = Json(ModelState)
        });
    }




    //we need to be able to call sales/sel/sales POST (SellUnit)
    //what to do with the result?
    //we can throw it in Hangfire here..
    // a job that queries for the unit in catalog and updates the status . . .
    //for now.. mae a call to HTTPclient here... to catalog api to udpate unit..

    private async Task<bool> IsABroker(string broker)
    {
        var brokers = await _accountService.GetBrokers();
        return brokers.Any() ? brokers.Select(i => i.Email).Contains(broker) : false;
    }

    private async Task<string[]> AdditionalValidationModelResult(string broker)
    {
        var validationErrors = new List<string>();
        //regardless of role --> check for broker validation
        if (_currentUser.IsBroker() && broker.ToUpper() != _currentUser.Email.ToUpper())
            validationErrors.Add("Current user should be assigned as the BROKER");

        if (_currentUser.IsAdmin())
            //check for the broker credentials if legit
            if (!string.IsNullOrEmpty(broker) && !await IsABroker(broker.ToUpper()))
                validationErrors.Add("BROKER not found.");

        return validationErrors.ToArray();
    }

    private async Task ValidateBroker(AddBuyerOrderRequest model)
    {
        var additionalValidation = await AdditionalValidationModelResult(model.Broker);

        if (additionalValidation.Any()) ModelState.AddModelError("Broker", string.Join(". ", additionalValidation));
    }

    private async Task ValidateFees(AddBuyerOrderRequest model)
    {
        if (model.Reservation > 0)
            if (string.IsNullOrEmpty(model.ReservationConfirmNo))
                ModelState.AddModelError(nameof(AddBuyerOrderRequest.ReservationConfirmNo),
                    "Reservation Confirmation number is required.");

        if (model.DownPayment > 0)
            if (string.IsNullOrEmpty(model.DownpaymentConfirmNo))
                ModelState.AddModelError(nameof(AddBuyerOrderRequest.DownpaymentConfirmNo),
                    "Downpayment Confirmation number is required.");
    }

}