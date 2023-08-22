using System.Collections.Generic;
using Common;
using Data.FakePlayer;

namespace Config
{

    public class Configs
    {
        public static void Init()
        {
<#list fileNames as name>
            ConfigLoader.Add("${name.confFileName}", ${name.confClassName}.Create);
</#list>
        }
    }

<#list fileContent as file>
${file}
</#list>
}