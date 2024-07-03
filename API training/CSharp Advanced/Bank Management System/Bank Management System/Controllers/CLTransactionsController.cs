using Bank_Management_System.Attributes;
using Bank_Management_System.Business_Logic;
using Bank_Management_System.Enums;
using Bank_Management_System.Models;
using Bank_Management_System.Models.DTO;
using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

/// <summary>
/// Controller for managing financial transactions.
/// </summary>
[JwtAuthorization]
public class CLTransactionsController : ApiController
{
    #region Private Member
    /// <summary>
    /// create the object of the transaction services
    /// </summary>
    private readonly BLTransaction _objBLTransaction;
    #endregion

    #region Public Member
    /// <summary>
    /// create the object of the response model
    /// </summary>
    public Response objResponse;
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the CLTransactionsController class.
    /// </summary>
    public CLTransactionsController()
    {
        _objBLTransaction = new BLTransaction();
        objResponse = new Response();
    }
    #endregion

    #region Private Method
    /// <summary>
    /// Gets the current user's ID from the claims.
    /// </summary>
    /// <returns>The current user's ID or 0 if not found.</returns>
    private int GetCurrentUser()
    {
        ClaimsPrincipal currentUser = User as ClaimsPrincipal;
        if (currentUser != null)
        {
            string userId = currentUser.FindFirst("Id")?.Value;

            return Convert.ToInt32(userId);
        }
        return 0;
    }
    #endregion

    #region Public Method

    /// <summary>
    /// Handles depositing money into a user's account.
    /// </summary>
    /// <param name="objTra">Transaction details for deposit.</param>
    /// <returns>HTTP response indicating success or failure.</returns>
    
    [HttpPost]
    [Route("api/transactions/deposit")]
    public IHttpActionResult DepositMoney([FromBody] DtoTra01 objDtoTra01)
    {
        int userId = GetCurrentUser();
        _objBLTransaction.TransactionType = enmTransactionTypes.D;
        _objBLTransaction.PreSave(userId, objDtoTra01);
        objResponse = _objBLTransaction.ValidationOnSave();
        if (!objResponse.IsError)
        {
            objResponse = _objBLTransaction.Save();
        }
        return Ok(objResponse);
    }

    /// <summary>
    /// Handles withdrawing money from a user's account.
    /// </summary>
    /// <param name="objTra">Transaction details for withdrawal.</param>
    /// <returns>HTTP response indicating success or failure.</returns>
    [HttpPost]
    [Route("api/transactions/withdraw")]
    public IHttpActionResult WithdrawMoney([FromBody] DtoTra01 objDtoTra01)
    {
        int userId = GetCurrentUser();
        _objBLTransaction.TransactionType = enmTransactionTypes.W;
        _objBLTransaction.PreSave(userId, objDtoTra01);
        objResponse = _objBLTransaction.ValidationOnSave();
        if (!objResponse.IsError)
        {
            objResponse = _objBLTransaction.Save();
        }
        return Ok(objResponse);
    }

    /// <summary>
    /// Retrieves details of all transactions (Admin-only access).
    /// </summary>
    /// <returns>HTTP response containing a list of transaction details.</returns>
    [Authorization(Roles = "A")]
    [HttpGet]
    [Route("api/transactions")]
    public IHttpActionResult GetAllTransactions()
    {
        objResponse = _objBLTransaction.GetAllTransactions();
        return Ok(objResponse);
    }

    /// <summary>
    /// Retrieves details of transactions for the currently logged-in user.
    /// </summary>
    /// <returns>HTTP response containing a list of transaction details.</returns>
    [Authorization(Roles = "U,A")]
    [HttpGet]
    [Route("api/transactions/details")]
    public IHttpActionResult GetTransactionByMe()
    {
        int userId = GetCurrentUser();
        objResponse = _objBLTransaction.GetTransactionByMe(userId);

        return Ok(objResponse);
    }

    /// <summary>
    /// Generates a statement of transactions for the currently logged-in user.
    /// </summary>
    /// <returns>HTTP response containing the transaction statement file.</returns>
    [Authorization(Roles = "U,A")]
    [HttpGet]
    [Route("api/statements")]
    public HttpResponseMessage Statements()
    {
        int userId = GetCurrentUser();
        return _objBLTransaction.Statements(userId, Request);
    }
    #endregion
}