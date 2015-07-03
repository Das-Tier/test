using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BeStyle.Repositories.Sql;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BeStyle.Admin.WebUI
{
    public partial class Default : System.Web.UI.Page
    {
        private string conStr = WebConfigurationManager.ConnectionStrings["BeStyleDB"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            BeStyleRepository BeStyleDB=new BeStyleRepository(conStr);
            GridView2.DataSource = BeStyleDB.GetAdminAll();
            GridView2.DataBind();
        }
    }
}