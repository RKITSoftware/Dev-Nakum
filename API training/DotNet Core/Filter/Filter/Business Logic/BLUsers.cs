using Filter.Model;

namespace Filter.Business_Logic
{
    public class BLUsers
    {
        #region Private Member
        private static int _id = 1;
        private static List<Use01> _lstUse01 = new List<Use01>();
        #endregion

        #region Public Method
        public void SignUp (Use01 objUse01)
        {
            objUse01.E01F01 = _id++;
            _lstUse01.Add(objUse01);
        }

        public object Login(Use01 objUse01)
        {
            Use01 user = _lstUse01.FirstOrDefault(u => u.E01F02 == objUse01.E01F02 && u.E01F03 == objUse01.E01F03);
            if (user == null)
            {
                return null;
            }
            BLAuth objBLAuth = new BLAuth();
            string token = objBLAuth.GenerateJWT(user.E01F01,user.E01F02,user.E01F04);
            
            return new
            {
                token,
                user.E01F04,
            };
        }

        public Use01 UserDetails(int id)
        {
            return _lstUse01.FirstOrDefault(u => u.E01F01 == id);
        }

        public List<Use01> AllUser()
        {
            return _lstUse01;
        }
        #endregion
    }
}
