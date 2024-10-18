using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AddressDAO
    {
        public int AddAddress(Address ads)
        {
			using (POSTDATAEntities db = new POSTDATAEntities())
			{
                try
                {
                    db.Addresses.Add(ads);
                    db.SaveChanges();
                    return ads.ID;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }			
        }
    }
}
