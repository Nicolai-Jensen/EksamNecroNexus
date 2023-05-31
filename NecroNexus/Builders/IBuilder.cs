using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    //--------------------------Nicolai Jensen----------------------------//
    public interface IBuilder
    {
        void BuildGameObject();

        GameObject GetResult();
    }
}
