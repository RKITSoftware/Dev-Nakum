using Bank_Management_System.Attributes;
using Bank_Management_System.Business_Logic;
using Bank_Management_System.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

/// <summary>
/// Controller for managing financial transactions.
/// </summary>
public class CLTransactionsController : ApiController
{
    #region Private Member
    private readonly BLTransaction _objBLTransaction;
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the CLTransactionsController class.
    /// </summary>
    public CLTransactionsController()
    {
        _objBLTransaction = new BLTransaction();
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
    [JwtAuthorization]
    [HttpPost]
    [Route("api/transactions/deposit")]
    public IHttpActionResult DepositMoney(Tra01 objTra)
    {
        int userId = GetCurrentUser();

        if (_objBLTransaction.AddTransaction(userId, objTra.A01F03, "Deposit"))
        {
            return Ok("Your money is deposited successfully");
        }
        else
        {
            return BadRequest("Something went wrong");
        }
    }

    /// <summary>
    /// Handles withdrawing money from a user's account.
    /// </summary>
    /// <param name="objTra">Transaction details for withdrawal.</param>
    /// <returns>HTTP response indicating success or failure.</returns>
    [JwtAuthorization]
    [HttpPost]
    [Route("api/transactions/withdraw")]
    public IHttpActionResult WithdrawMoney(Tra01 objTra)
    {
        int userId = GetCurrentUser();

        if (_objBLTransaction.AddTransaction(userId, objTra.A01F03, "Withdraw"))
        {
            return Ok("Your money is withdrawn successfully");
        }
        else
        {
            return BadRequest("Something went wrong");
        }
    }

    /// <summary>
    /// Retrieves details of all transactions (Admin-only access).
    /// </summary>
    /// <returns>HTTP response containing a list of transaction details.</returns>
    [JwtAuthorization]
    [Authorization(Roles = "Admin")]
    [HttpGet]
    [Route("api/transactions")]
    public IHttpActionResult GetAllTransactions()
    {
        return Ok(_objBLTransaction.GetAllTransactions());
    }

    /// <summary>
    /// Retrieves details of transactions for the currently logged-in user.
    /// </summary>
    /// <returns>HTTP response containing a list of transaction details.</returns>
    [JwtAuthorization]
    [Authorization(Roles = "User")]
    [HttpGet]
    [Route("api/transactions/me")]
    public IHttpActionResult GetTransactionByMe()
    {
        int userId = GetCurrentUser();
        return Ok(_objBLTransaction.GetTransactionByMe(userId));
    }

    /// <summary>
    /// Generates a statement of transactions for the currently logged-in user.
    /// </summary>
    /// <returns>HTTP response containing the transaction statement file.</returns>
    [JwtAuthorization]
    [Authorization(Roles = "User")]
    [HttpGet]
    [Route("api/statements/me")]
    public HttpResponseMessage Statements()
    {
        int userId = GetCurrentUser();
        return _objBLTransaction.Statements(userId, Request);
    }
    #endregion
}