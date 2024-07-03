using Dependency_Injection.Interface;
using Dependency_Injection.Model;
using Microsoft.AspNetCore.Mvc;

namespace Dependency_Injection.Controllers
{
    /// <summary>
    /// controller can manage the transaction related api
    /// </summary>
    [Route("api/transactions")]
    [ApiController]
    public class CLBankController : ControllerBase
    {
        #region Private member

        /// <summary>
        /// create the object of the bank interface
        /// </summary>
        private readonly IBank _bank;
        #endregion

        #region Constuctor

        /// <summary>
        /// initialize the object of the bank interface
        /// </summary>
        /// <param name="bank"></param>
        public CLBankController(IBank bank)
        {
            _bank = bank;
        }

        public CLBankController()
        {

        }
        #endregion

        #region Public Method

        /// <summary>
        /// add transaction into list and update the user's amount
        /// </summary>
        /// <param name="user">resolve the service dependency into action method</param>
        /// <param name="objBan01">object of the bank</param>
        /// <returns>updated user's details</returns>
        [HttpPost]
        public IActionResult AddTransaction([FromServices] IUsers user, Ban01 objBan01)
        {
            // [fromServices] -- resolve the service dependency directly in action method, only available in particular action method

            // add transaction into list
            bool transaction = _bank.AddTransaction(objBan01);   
            if (transaction)
            {
                // update the user's money
                bool isUserUpdated = user.UpdateUser(objBan01.N01F01, objBan01.N01F03, objBan01.N01F02);

                if(!isUserUpdated)
                {
                    return BadRequest("invalid account number");
                }
                //get the updated user
                Use01 updatedUser = user.GetUserById(objBan01.N01F01);
                return Ok(updatedUser);
            }
            else
            {
                return BadRequest("something went wrong");
            }
        }
        #endregion

    }
}
