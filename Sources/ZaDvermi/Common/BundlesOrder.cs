
using System.Collections.Generic;
using System.Web.Optimization;

namespace ZaDvermi.Common
{
    public class BundlesOrder : IBundleOrderer
    {
        public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
        {
            return files;
        }
    }
}