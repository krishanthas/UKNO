using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFRS.DAL.FileManage
{
    public class CustomerCsv
    {

        public string Export()
        {
            try
            {
                var cus = DataContext.GetAllCustomers();
                return CsvSerializer.SerializeToCsv(cus);
            }
            catch (Exception ex)
            {

            }
            return string.Empty;
        }

        public List<Customer> Import(string csv)
        {
            try
            {
                var custList = CsvSerializer.DeserializeFromString<List<Customer>>(csv);

                return custList;
            }
            catch (Exception ex)
            {
                
            }
            return null;
        }
    }
}
