using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace EVSTAR.Models
{
    public class Coverages : CollectionBase
    {
        public Coverages()
        {
        }

        public Coverages(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Coverage cov = new Coverage(row);
                    Add(cov);
                }
            }
        }

        public int Add(Coverage aCoverage)
        {
            return List.Add(aCoverage);
        }

        public void Remove(int index)
        {
            // Check to see if there is a Coverage at the supplied index.
            if (index > Count - 1 || index < 0)
            {
                //System.Windows.Forms.MessageBox.Show("Index not valid!");
            }
            else
            {
                List.RemoveAt(index);
            }
        }

        // Indexer implementation.
        public Coverage this[int index]
        {
            get
            {
                // Code to return a Coverage.
                return (Coverage)List[index];
            }
            set
            {
                // Code to set a Coverage.
                List[index] = value;
            }
        }

    }
}
