using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.BlobStoring;
using Volo.Abp.Modularity;

namespace Wei.Abp.BlobStoring.TencentCloudCos
{
    [DependsOn(typeof(AbpBlobStoringModule))]
    public class BlobStoringTencentCloudCosModule: AbpModule
    {

    }
}
